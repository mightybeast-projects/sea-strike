using System.Net.Mime;
using System.Threading;
using System;
using System.Net;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : GameScreen
{
    private SeaStrike game;
    private const int port = 1111;
    private const string address = "127.0.0.1";

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

    public override void Update(GameTime gameTime) { }

    private void CreateNewLobby()
    {
        var server = new SeaStrikeServer(IPAddress.Any, port, game);

        server.OptionNoDelay = true;
        server.OptionTcpKeepAliveInterval = 1;

        // Start the server
        Console.Write("Server starting...");
        server.Start();
        Console.WriteLine("Done!");

        Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");
    }

    private void ConnectToLobby()
    {
        var client = new SeaStrikeClient(address, port, game);

        // Connect the client
        Console.Write("Client connecting...");
        bool connected = client.ConnectAsync();

        Console.WriteLine("Done!");

        Console.WriteLine("Press Enter to stop the client or '!' to reconnect the client...");
    }
}