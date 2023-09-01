using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer : TcpServer
{
    private TcpSession session;
    private SeaStrike game;

    public SeaStrikeServer(IPAddress address, int port, SeaStrike game) :
        base(address, port) => this.game = game;

    protected override TcpSession CreateSession()
    {
        if (session == null)
            session = new SeaStrikeSession(this);

        return session;
    }

    protected override void OnConnected(TcpSession session)
    {
        game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));
    }

    protected override void OnError(SocketError error) =>
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
}