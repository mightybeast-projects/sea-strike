using System;
using LiteNetLib;
using LiteNetLib.Utils;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient
{
    private Player player;
    private NetManager client;
    private SeaStrike game => player.game;

    public SeaStrikeClient(Player player)
    {
        this.player = player;

        player.client = this;

        EventBasedNetListener listener = new EventBasedNetListener();
        client = new NetManager(listener);

        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(fromPeer, dataReader, deliveryMethod, channel);
        listener.PeerDisconnectedEvent += (peer, info) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));
    }

    public void Start()
    {
        client.Start();
        client.Connect(Utils.address, 9050, Utils.connectionKey);
    }

    public void Disconnect() => client.DisconnectAll();

    public void PollEvents() => client.PollEvents();

    public void Send(string message) =>
        client.SendToAll(FormMessage(message), DeliveryMethod.ReliableOrdered);

    private void HandleReceivedMessage(
        NetPeer fromPeer,
        NetPacketReader dataReader,
        byte deliveryMethod,
        DeliveryMethod channel)
    {
        string message = dataReader.GetString(100);

        Console.WriteLine("From server: {0}", message);

        if (message == Utils.deploymentPhaseStartMessage)
            game.screenManager
                .LoadScreen(new MultiplayerDeploymentPhaseScreen(player));

        dataReader.Recycle();
    }

    private NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }
}