using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer : TcpServer
{
    public SeaStrikeServer(IPAddress address, int port) :
        base(address, port)
    { }

    protected override TcpSession CreateSession() => new SeaStrikeSession(this);

    protected override void OnError(SocketError error) =>
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
}