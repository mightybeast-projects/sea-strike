using System;
using System.Text;
using System.Threading;
using NetCoreServer;
using SeaStrike.PC.Root.Screens;
using SocketError = System.Net.Sockets.SocketError;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient : TcpClient
{
    private SeaStrike game;
    private bool _stop;

    public SeaStrikeClient(string address, int port, SeaStrike game)
        : base(address, port) => this.game = game;

    public void DisconnectAndStop()
    {
        _stop = true;
        DisconnectAsync();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected() =>
        Console.WriteLine($"Client : Chat TCP client connected a new session with Id {Id}");

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        System.Console.WriteLine("Message from server : " + message);

        if (message == "-> Deploy ships")
            game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP client caught an error with code {error}");
    }
}