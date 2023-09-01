using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer : TcpServer
{
    private SeaStrike game;

    public SeaStrikeServer(IPAddress address, int port, SeaStrike game) :
        base(address, port) => this.game = game;

    protected override TcpSession CreateSession() => new SeaStrikeSession(this);

    protected override void OnError(SocketError error) =>
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
}