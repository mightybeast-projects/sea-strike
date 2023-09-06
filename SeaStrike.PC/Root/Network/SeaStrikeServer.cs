using System.Collections.Generic;
using System;
using LiteNetLib;
using System.Linq;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer : SeaStrikeNetManager
{
    private Dictionary<NetPeer, string> playerBoardDatas;

    private bool gameStarted => player.seaStrikeGame is not null;

    public SeaStrikeServer(NetPlayer player) : base(player)
    {
        player.server = this;
        playerBoardDatas = new Dictionary<NetPeer, string>();

        EventBasedNetListener listener = new EventBasedNetListener();

        listener.ConnectionRequestEvent += request => HandleNewRequest(request);
        listener.PeerConnectedEvent += peer => HandleNewPeer(peer);
        listener.PeerDisconnectedEvent += (peer, info) =>
            player.RedirectToMainMenu();
        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(fromPeer, dataReader);

        netManager = new NetManager(listener);
    }

    public void Start() => netManager.Start(NetUtils.port);

    public void Disconnect()
    {
        netManager.DisconnectAll();
        netManager.Stop();
    }

    public void PollEvents() => netManager.PollEvents();

    private void HandleNewRequest(ConnectionRequest request)
    {
        if (netManager.ConnectedPeersCount < 2)
            request.AcceptIfKey(NetUtils.connectionKey);
        else
            request.Reject();
    }

    private void HandleNewPeer(NetPeer peer)
    {
        Console.WriteLine("New connection: {0}", peer.EndPoint);

        if (netManager.ConnectedPeersCount == 2)
            StartDeploymentPhase();
    }

    private void HandleReceivedMessage(
        NetPeer fromPeer,
        NetPacketReader dataReader)
    {
        string message = dataReader.GetString();

        if (MessageIsBoardData(message))
        {
            playerBoardDatas.Add(fromPeer, message);

            if (playerBoardDatas.Count == 2)
            {
                ExchangeBoardDatas();
                StartBattlePhase();
            }
        }

        if (gameStarted)
            SendShotTile(fromPeer, message);
    }

    private void ExchangeBoardDatas() =>
        playerBoardDatas.ToList().ForEach(
            playerData => SendOpponentBoard(playerData));

    private void StartDeploymentPhase() =>
        netManager.SendToAll(
            FormMessage(NetUtils.deploymentPhaseStartMessage),
            DeliveryMethod.ReliableOrdered);

    private void SendOpponentBoard(KeyValuePair<NetPeer, string> item) =>
        netManager.SendToAll(
            FormMessage(item.Value),
            DeliveryMethod.ReliableOrdered,
            item.Key);

    private void StartBattlePhase() =>
        netManager.SendToAll(
            FormMessage(NetUtils.startBattlePhaseMessage),
            DeliveryMethod.ReliableOrdered);

    private void SendShotTile(NetPeer fromPeer, string tileStr) =>
        netManager.SendToAll(
            FormMessage(tileStr),
            DeliveryMethod.ReliableOrdered,
            fromPeer);
}