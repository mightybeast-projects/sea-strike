using System;
using System.Text;
using System.Threading;
using NetCoreServer;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient : TcpClient
{
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

        // Wait for a while...
        Thread.Sleep(1000);

        // Try to connect again
        if (!_stop)
            ConnectAsync();
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        Console.WriteLine("From client : " + Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
    }

    private bool _stop;
}