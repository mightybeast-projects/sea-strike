using System;
using LiteNetLib;
using LiteNetLib.Utils;
using Newtonsoft.Json;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Screens.Multiplayer;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient
{
    private NetPlayer player;
    private NetManager client;
    private BoardData data;
    private SeaStrike game => player.game;

    public SeaStrikeClient(NetPlayer player)
    {
        this.player = player;

        player.client = this;

        EventBasedNetListener listener = new EventBasedNetListener();
        client = new NetManager(listener);

        listener.NetworkReceiveEvent +=
            (fromPeer, dataReader, deliveryMethod, channel) =>
                HandleReceivedMessage(dataReader);
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

    private void HandleReceivedMessage(NetPacketReader dataReader)
    {
        string message = dataReader.GetString();

        Console.WriteLine("From server: {0}", message);

        if (message == Utils.deploymentPhaseStartMessage)
            game.screenManager
                .LoadScreen(new MultiplayerDeploymentPhaseScreen(player));
        else if (message == Utils.startBattlePhaseMessage)
            game.screenManager
                .LoadScreen(new MultiplayerBattlePhaseScreen(
                    player, data.Build()));
        else if (message.Length > 3)
            data = JsonConvert.DeserializeObject<BoardData>(message);
        else
        {
            player.seaStrikeGame.HandleCurrentPlayerShot(message);

            if (player.seaStrikeGame.isOver)
                game.ShowLostScreen();

            player.canShoot = true;
        }

        dataReader.Recycle();
    }

    private NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }
}