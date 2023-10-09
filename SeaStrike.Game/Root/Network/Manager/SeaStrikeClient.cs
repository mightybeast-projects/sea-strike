using LiteNetLib;
using SeaStrike.PC.Root.Network.Listener;

namespace SeaStrike.PC.Root.Network.Manager;

public class SeaStrikeClient : NetManager
{
    public SeaStrikeClient(SeaStrikeClientListener listener) : base(listener)
    {
        listener.client = this;

        UnconnectedMessagesEnabled = true;
        DisconnectTimeout = 3000;
        IPv6Enabled = false;
        ReuseAddress = true;
    }

    public new void Start()
    {
        if (!IsRunning)
            base.Start();

        var result = base.SendBroadcast(
            new SeaStrikeNetDataWriter(NetUtils.serverDiscoveryMessage),
            NetUtils.port);
    }

    public void Send(string message) =>
        SendToAll(
            new SeaStrikeNetDataWriter(message),
            DeliveryMethod.ReliableOrdered);
}