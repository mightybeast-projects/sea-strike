using System.Linq;
using System.Collections.Generic;
using System;
using LiteNetLib;
using LiteNetLib.Utils;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer
{
    private Player player;
    private NetManager server;
    private Dictionary<NetPeer, string> playerBoardDatas;
    private SeaStrike game => player.game;

    public SeaStrikeServer(Player player)
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

        peer.Send(FormMessage("Hello client!"), DeliveryMethod.ReliableOrdered);

        if (server.ConnectedPeersCount == 2)
            StartDeploymentPhase();
    }

    private void HandleReceivedMessage(
        NetPeer fromPeer,
        NetPacketReader dataReader)
    {
        string message = dataReader.GetString();

        //Console.WriteLine("From client {0}: {1}", fromPeer.Id, message);

        playerBoardDatas.Add(fromPeer, message);

        if (playerBoardDatas.Count == 2)
            ExchangeBoardDatas();
    }

    private void ExchangeBoardDatas()
    {
        foreach (KeyValuePair<NetPeer, string> item in playerBoardDatas)
            server.SendToAll(
                FormMessage(item.Value),
                DeliveryMethod.ReliableOrdered,
                item.Key);
    }

    private NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }

    private void StartDeploymentPhase() =>
        server.SendToAll(
            FormMessage(Utils.deploymentPhaseStartMessage),
            DeliveryMethod.ReliableOrdered);
}