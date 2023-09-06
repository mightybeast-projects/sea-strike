using System.Collections.Generic;
using System;
using LiteNetLib;
using LiteNetLib.Utils;
using SeaStrike.PC.Root.Screens;
using System.Linq;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer
{
    private NetPlayer player;
    private NetManager server;
    private Dictionary<NetPeer, string> playerBoardDatas;
    private SeaStrike game => player.game;
    private bool gameStarted => player.seaStrikeGame is not null;

    public SeaStrikeServer(NetPlayer player)
    {
        this.player = player;

        player.server = this;
        playerBoardDatas = new Dictionary<NetPeer, string>();

        EventBasedNetListener listener = new EventBasedNetListener();
        server = new NetManager(listener);

        listener.ConnectionRequestEvent += request => HandleNewRequest(request);
        listener.PeerConnectedEvent += peer => HandleNewPeer(peer);
        listener.PeerDisconnectedEvent += (peer, info) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));
        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(fromPeer, dataReader);
    }

    public void Start() => server.Start(Utils.port);

    public void Disconnect()
    {
        server.DisconnectAll();
        server.Stop();
    }

    public void PollEvents() => server.PollEvents();

    private void HandleNewRequest(ConnectionRequest request)
    {
        if (server.ConnectedPeersCount < 2)
            request.AcceptIfKey(Utils.connectionKey);
        else
            request.Reject();
    }

    private void HandleNewPeer(NetPeer peer)
    {
        Console.WriteLine("New connection: {0}", peer.EndPoint);

        if (server.ConnectedPeersCount == 2)
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
        playerBoardDatas.ToList()
        .ForEach(playerData => SendOpponentBoard(playerData));

    private void StartDeploymentPhase() =>
        server.SendToAll(
            FormMessage(Utils.deploymentPhaseStartMessage),
            DeliveryMethod.ReliableOrdered);

    private void SendOpponentBoard(KeyValuePair<NetPeer, string> item) =>
        server.SendToAll(
            FormMessage(item.Value),
            DeliveryMethod.ReliableOrdered,
            item.Key);

    private void StartBattlePhase() =>
        server.SendToAll(
            FormMessage(Utils.startBattlePhaseMessage),
            DeliveryMethod.ReliableOrdered);

    private void SendShotTile(NetPeer fromPeer, string tileStr) =>
        server.SendToAll(
            FormMessage(tileStr),
            DeliveryMethod.ReliableOrdered,
            fromPeer);

    private NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }

    private bool MessageIsBoardData(string message) =>
        message.StartsWith('{') && message.EndsWith('}');
}