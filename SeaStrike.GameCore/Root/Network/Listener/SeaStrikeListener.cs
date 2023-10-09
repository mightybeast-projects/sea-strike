using System.Net;
using System.Net.Sockets;
using LiteNetLib;

namespace SeaStrike.PC.Root.Network.Listener;

public abstract class SeaStrikeListener : INetEventListener
{
    protected NetPlayer player;

    public SeaStrikeListener(NetPlayer player) => this.player = player;

    public virtual void OnConnectionRequest(ConnectionRequest request) { }

    public virtual void OnNetworkError(
        IPEndPoint endPoint,
        SocketError socketError)
    { }

    public virtual void OnNetworkLatencyUpdate(NetPeer peer, int latency) { }

    public virtual void OnNetworkReceive(
        NetPeer peer,
        NetPacketReader reader,
        byte channelNumber,
        DeliveryMethod deliveryMethod)
    { }

    public virtual void OnNetworkReceiveUnconnected(
        IPEndPoint remoteEndPoint,
        NetPacketReader reader,
        UnconnectedMessageType messageType)
    { }

    public virtual void OnPeerConnected(NetPeer peer) { }

    public virtual void OnPeerDisconnected(
        NetPeer peer,
        DisconnectInfo disconnectInfo)
    { }

    protected bool MessageIsBoardData(string message) =>
        message.StartsWith('{') && message.EndsWith('}');
}