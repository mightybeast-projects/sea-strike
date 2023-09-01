using System;
using System.Text;
using System.Threading;
using NetCoreServer;
using SocketError = System.Net.Sockets.SocketError;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient : TcpClient
{
    private bool _stop;

    public SeaStrikeClient(string address, int port) : base(address, port) { }

    public void DisconnectAndStop()
    {
        _stop = true;
        DisconnectAsync();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected() =>
        Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        Console.WriteLine("From client : " + Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP client caught an error with code {error}");
    }
}