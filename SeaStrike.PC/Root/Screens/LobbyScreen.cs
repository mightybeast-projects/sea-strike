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
    public LobbyScreen(SeaStrike game) : base(game)
    {
        game.desktop.Root = new Label() { Text = "Lobby" };

        int port = 1111;
        string address = "127.0.0.1";

        Console.WriteLine($"TCP server port: {port}");

        Console.WriteLine();

        // Create a new TCP chat server
        var server = new SeaStrikeServer(IPAddress.Parse(address), port);

        // Start the server
        Console.Write("Server starting...");
        server.Start();
        Console.WriteLine("Done!");

        Console.WriteLine($"TCP server address: {address}");
        Console.WriteLine($"TCP server port: {port}");

        Console.WriteLine();

        // Create a new TCP chat client
        var client = new SeaStrikeClient(address, port);

        // Connect the client
        Console.Write("Client connecting...");
        client.Connect();
        Console.WriteLine("Done!");


        client.SendAsync("Hello");
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}