using System;
using System.Net;
using LiteNetLib;
using SeaStrike.PC.Root.Network.Manager;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Screens.Multiplayer;

namespace SeaStrike.PC.Root.Network.Listener;

public class SeaStrikeClientListener : SeaStrikeListener
{
    internal SeaStrikeClient client;
    private bool connected;

    public SeaStrikeClientListener(NetPlayer player) : base(player) { }

    public override void OnNetworkReceiveUnconnected(
        IPEndPoint remoteEndPoint,
        NetPacketReader reader,
        UnconnectedMessageType messageType)
    {
        if (ServerDiscovered(reader.GetString(), messageType))
        {
            connected = true;
            client.Connect(remoteEndPoint, NetUtils.connectionKey);
        }
    }

    public override void OnNetworkReceive(
        NetPeer peer,
        NetPacketReader reader,
        byte channelNumber,
        DeliveryMethod deliveryMethod)
    {
        string message = reader.GetString();

        Console.WriteLine("From server: {0}", message);

        if (message == NetUtils.deploymentPhaseStartMessage)
            player.RedirectTo<NetDeploymentPhaseScreen>();
        else if (message == NetUtils.startBattlePhaseMessage)
            player.RedirectTo<NetBattlePhaseScreen>();
        else if (MessageIsBoardData(message))
            player.ReceiveOpponentBoardData(message);
        else
            player.HandleOpponentShot(message);

        reader.Recycle();
    }

    public override void OnPeerDisconnected(
        NetPeer peer,
        DisconnectInfo disconnectInfo)
            => player.RedirectTo<MainMenuScreen>();

    public override void OnConnectionRequest(ConnectionRequest request) =>
        request.Reject();

    private bool ServerDiscovered(
        string message,
        UnconnectedMessageType messageType) =>
            message == NetUtils.discoveredServerMessage &&
            messageType == UnconnectedMessageType.BasicMessage &&
            !connected;
}