using System;
using LiteNetLib;
using LiteNetLib.Utils;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer
{
    private SeaStrike game;
    private NetManager server;

    public SeaStrikeServer(SeaStrike game)
    {
        this.game = game;

        EventBasedNetListener listener = new EventBasedNetListener();
        server = new NetManager(listener);
        server.Start(Utils.port);

        System.Console.WriteLine("Server started!");

        listener.ConnectionRequestEvent += request =>
            HandleNewRequest(request);
        listener.PeerConnectedEvent += peer => HandleNewPeer(peer);
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