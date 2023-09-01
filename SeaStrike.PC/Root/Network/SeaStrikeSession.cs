using System;
using System.Net.Sockets;
using System.Text;
using NetCoreServer;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeSession : TcpSession
{
    public SeaStrikeSession(TcpServer server) : base(server) { }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} connected!");

        if (Server.ConnectedSessions == 2)
            Server.Multicast("-> Deploy ships");
    }

    protected override void OnDisconnected() =>
        Console.WriteLine($"Chat TCP session with Id {Id} disconnected!");

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        Console.WriteLine("Incoming: " + message);

        // Multicast message to all connected sessions
        Server.Multicast(message);

        // If the buffer starts with '!' the disconnect the current session
        if (message == "!")
            Disconnect();
    }

    protected override void OnError(SocketError error) =>
        Console.WriteLine($"Chat TCP session caught an error with code {error}");
}