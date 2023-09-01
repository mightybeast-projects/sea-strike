using System.Threading;
using System;
using System.Net;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Network;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : GameScreen
{
    private const int port = 1111;
    private const string address = "127.0.0.1";

    public LobbyScreen(SeaStrike game) : base(game)
    {
        game.desktop.Root = new Label() { Text = "Lobby" };
    }

    public override void LoadContent()
    {
        base.LoadContent();

        CreateNewClient();
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }

    private void CreateNewClient()
    {
        var client = new SeaStrikeClient(address, port);

        bool connected = client.Connect();

        if (!connected)
        {
            System.Console.WriteLine("No created server. Creating one...");
            CreateNewServer();

            client = new SeaStrikeClient(address, port);
            Console.Write("Client connecting...");
            client.Connect();
            Console.WriteLine("Done!");

            client.SendAsync("Hello");
        }
        else
        {
            Console.Write("Client connected");
            Console.WriteLine("Done!");

            client.SendAsync("Hello");
        }
    }

    private void CreateNewServer()
    {
        Console.WriteLine($"TCP server port: {port}");

        Console.WriteLine();

        // Create a new TCP chat server
        var server = new SeaStrikeServer(IPAddress.Parse(address), port);

        // Start the server
        Console.Write("Server starting...");

        server.Start();
        server.OptionReuseAddress = true;
        Console.WriteLine("Done!");

        Console.WriteLine($"TCP server address: {address}");
        Console.WriteLine($"TCP server port: {port}");

        Console.WriteLine();
    }
}