using LiteNetLib;
using SeaStrike.GameCore.Root.Network.Listener;

namespace SeaStrike.GameCore.Root.Network.Manager;

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
    }

    public void Send(string message) =>
        SendToAll(
            new SeaStrikeNetDataWriter(message),
            DeliveryMethod.ReliableOrdered);
}