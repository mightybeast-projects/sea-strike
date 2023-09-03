using System;
using LiteNetLib;
using LiteNetLib.Utils;
using Newtonsoft.Json;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer
{
    private Player player;
    private NetManager server;
    private SeaStrike game => player.game;

    public SeaStrikeServer(Player player)
    {
        this.player = player;

        player.server = this;

        EventBasedNetListener listener = new EventBasedNetListener();
        server = new NetManager(listener);

        listener.ConnectionRequestEvent += request => HandleNewRequest(request);
        listener.PeerConnectedEvent += peer => HandleNewPeer(peer);
        listener.PeerDisconnectedEvent += (peer, info) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));
        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(fromPeer, dataReader, deliveryMethod, channel);
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
        NetPacketReader dataReader,
        byte deliveryMethod,
        DeliveryMethod channel)
    {
        string message = dataReader.GetString();

        Console.WriteLine("From client {0}: {1}", fromPeer.Id, message);

        Board playerBoard = JsonConvert.DeserializeObject<Board>(message);
        System.Console.WriteLine(playerBoard.oceanGrid.tiles[0, 0].isOccupied);
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