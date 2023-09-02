using System.Net.Mime;
using System.Threading;
using System;
using System.Net;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;
using LiteNetLib;
using LiteNetLib.Utils;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : GameScreen
{
    private SeaStrike game;
    private NetManager server;
    private NetManager client;

    public LobbyScreen(SeaStrike game) : base(game)
    {
        this.game = game;

        VerticalStackPanel panel = new VerticalStackPanel()
        {
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel.Widgets.Add(new Label()
        {
            Text = "Lobby screen",
            HorizontalAlignment = HorizontalAlignment.Center,
        });

        panel.Widgets.Add(new GameButton(() => CreateNewLobby())
        {
            Text = "Create new lobby"
        });

        panel.Widgets.Add(new GameButton(() => ConnectToLobby())
        {
            Text = "Connect to existing lobby"
        });

        game.desktop.Root = panel;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime)
    {
        if (server is not null)
            server.PollEvents();
        if (client is not null)
            client.PollEvents();
    }

    private void CreateNewLobby()
    {
        EventBasedNetListener listener = new EventBasedNetListener();
        server = new NetManager(listener);
        server.Start(9050 /* port */);

        System.Console.WriteLine("Server started!");

        listener.ConnectionRequestEvent += request =>
        {
            if (server.ConnectedPeersCount < 10 /* max connections */)
                request.AcceptIfKey("SomeConnectionKey");
            else
                request.Reject();
        };

        listener.PeerConnectedEvent += peer =>
        {
            Console.WriteLine("New connection: {0}", peer.EndPoint); // Show peer ip
            NetDataWriter writer = new NetDataWriter();                 // Create writer class
            writer.Put("Hello client!");                                // Put some string
            peer.Send(writer, DeliveryMethod.ReliableOrdered);

            if (server.ConnectedPeersCount == 2)
            {
                writer = new NetDataWriter();                 // Create writer class
                writer.Put("-> Deploy ships");
                server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
            }
        };

        ConnectToLobby();
    }

    private void ConnectToLobby()
    {
        EventBasedNetListener listener = new EventBasedNetListener();
        client = new NetManager(listener);
        client.Start();
        client.Connect("localhost" /* host ip or name */, 9050 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);

        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
        {
            string message = dataReader.GetString(100);

            Console.WriteLine("From server: {0}", message);

            if (message == "-> Deploy ships")
                game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));

            dataReader.Recycle();
        };
    }
}