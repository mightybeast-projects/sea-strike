using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClientListener : INetEventListener
{
    private NetPlayer player;

    public SeaStrikeClientListener(NetPlayer player) => this.player = player;

    public void OnNetworkReceive(
        NetPeer peer,
        NetPacketReader reader,
        byte channelNumber,
        DeliveryMethod deliveryMethod)
    {
        string message = reader.GetString();

        Console.WriteLine("From server: {0}", message);

        if (message == NetUtils.deploymentPhaseStartMessage)
            player.RedirectToDeploymentScreen();
        else if (message == NetUtils.startBattlePhaseMessage)
            player.RedirectToBattleScreen();
        else if (MessageIsBoardData(message))
            player.ReceiveOpponentBoardData(message);
        else
            player.HandleOpponentShot(message);

        reader.Recycle();
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        => player.RedirectToMainMenu();

    public void OnConnectionRequest(ConnectionRequest request) { }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError) { }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency) { }

    public void OnNetworkReceiveUnconnected(
        IPEndPoint remoteEndPoint,
        NetPacketReader reader,
        UnconnectedMessageType messageType)
    { }

    public void OnPeerConnected(NetPeer peer) { }

    protected bool MessageIsBoardData(string message) =>
        message.StartsWith('{') && message.EndsWith('}');
}