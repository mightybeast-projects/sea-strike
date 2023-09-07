using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServerListener : INetEventListener
{
    internal NetManager server;

    private NetPlayer player;
    private Dictionary<NetPeer, string> playerBoardDatas;

    private bool gameStarted => player.seaStrikeGame is not null;

    public SeaStrikeServerListener(NetPlayer player)
    {
        this.player = player;

        playerBoardDatas = new Dictionary<NetPeer, string>();
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        if (server.ConnectedPeersCount < 2)
            request.AcceptIfKey(NetUtils.connectionKey);
        else
            request.Reject();
    }

    public void OnNetworkReceive(
        NetPeer peer,
        NetPacketReader reader,
        byte channelNumber,
        DeliveryMethod deliveryMethod)
    {
        string message = reader.GetString();

        if (MessageIsBoardData(message))
        {
            playerBoardDatas.Add(peer, message);

            if (playerBoardDatas.Count == 2)
            {
                ExchangeBoardDatas();
                StartBattlePhase();
            }
        }

        if (gameStarted)
            SendShotTile(peer, message);
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine("New connection: {0}", peer.EndPoint);

        if (server.ConnectedPeersCount == 2)
            StartDeploymentPhase();
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        => player.RedirectToMainMenu();

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError) { }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency) { }

    public void OnNetworkReceiveUnconnected(
        IPEndPoint remoteEndPoint,
        NetPacketReader reader,
        UnconnectedMessageType messageType)
    { }

    private void StartDeploymentPhase() =>
        server.SendToAll(
            FormMessage(NetUtils.deploymentPhaseStartMessage),
            DeliveryMethod.ReliableOrdered);

    private void ExchangeBoardDatas() =>
        playerBoardDatas.ToList().ForEach(
            playerData => SendOpponentBoard(playerData));

    private void SendOpponentBoard(KeyValuePair<NetPeer, string> item) =>
        server.SendToAll(
            FormMessage(item.Value),
            DeliveryMethod.ReliableOrdered,
            item.Key);

    private void StartBattlePhase() =>
        server.SendToAll(
            FormMessage(NetUtils.startBattlePhaseMessage),
            DeliveryMethod.ReliableOrdered);

    private void SendShotTile(NetPeer fromPeer, string tileStr) =>
        server.SendToAll(
            FormMessage(tileStr),
            DeliveryMethod.ReliableOrdered,
            fromPeer);

    protected NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }

    protected bool MessageIsBoardData(string message) =>
        message.StartsWith('{') && message.EndsWith('}');
}