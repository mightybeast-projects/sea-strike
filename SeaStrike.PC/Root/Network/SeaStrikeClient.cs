using System;
using LiteNetLib;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient
{
    private SeaStrike game;
    private NetManager client;

    public SeaStrikeClient(SeaStrike game)
    {
        this.game = game;

        EventBasedNetListener listener = new EventBasedNetListener();
        client = new NetManager(listener);

        client.Start();
        client.Connect(Utils.address, 9050, Utils.connectionKey);

        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(fromPeer, dataReader, deliveryMethod, channel);
    }

    public void PollEvents() => client.PollEvents();

    private void HandleReceivedMessage(
        NetPeer fromPeer,
        NetPacketReader dataReader,
        byte deliveryMethod,
        DeliveryMethod channel)
    {
        string message = dataReader.GetString(100);

        Console.WriteLine("From server: {0}", message);

        if (message == Utils.deploymentPhaseStartMessage)
            game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));

        dataReader.Recycle();
    }
}