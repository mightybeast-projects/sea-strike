using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LiteNetLib;

namespace SeaStrike.PC.Root.Network.Listener;

public class SeaStrikeServerListener : SeaStrikeListener
{
    internal NetManager server;

    private Dictionary<NetPeer, string> playerBoardDatas;

    private bool gameStarted => player.seaStrikeGame is not null;

    public SeaStrikeServerListener(NetPlayer player) : base(player) =>
        playerBoardDatas = new Dictionary<NetPeer, string>();

    public override void OnNetworkReceiveUnconnected(
        IPEndPoint remoteEndPoint,
        NetPacketReader reader,
        UnconnectedMessageType messageType)
    {
        server.SendUnconnectedMessage(
            new SeaStrikeNetDataWriter(NetUtils.discoveredServerMessage),
            remoteEndPoint);
    }

    public override void OnConnectionRequest(ConnectionRequest request)
    {
        if (server.ConnectedPeersCount < 2)
            request.AcceptIfKey(NetUtils.connectionKey);
        else
            request.Reject();
    }

    public override void OnNetworkReceive(
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

    public override void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine("New connection: {0}", peer.EndPoint);

        if (server.ConnectedPeersCount == 2)
            StartDeploymentPhase();
    }

    public override void OnPeerDisconnected(
        NetPeer peer,
        DisconnectInfo disconnectInfo) => player.RedirectToMainMenu();

    private void StartDeploymentPhase() =>
        server.SendToAll(
            new SeaStrikeNetDataWriter(NetUtils.deploymentPhaseStartMessage),
            DeliveryMethod.ReliableOrdered);

    private void ExchangeBoardDatas() =>
        playerBoardDatas.ToList().ForEach(
            playerData => SendOpponentBoard(playerData));

    private void SendOpponentBoard(KeyValuePair<NetPeer, string> item) =>
        server.SendToAll(
            new SeaStrikeNetDataWriter(item.Value),
            DeliveryMethod.ReliableOrdered,
            item.Key);

    private void StartBattlePhase() =>
        server.SendToAll(
            new SeaStrikeNetDataWriter(NetUtils.startBattlePhaseMessage),
            DeliveryMethod.ReliableOrdered);

    private void SendShotTile(NetPeer fromPeer, string tileStr) =>
        server.SendToAll(
            new SeaStrikeNetDataWriter(tileStr),
            DeliveryMethod.ReliableOrdered,
            fromPeer);
}