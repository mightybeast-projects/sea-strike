using LiteNetLib;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient : NetManager
{
    public SeaStrikeClient(SeaStrikeClientListener listener) : base(listener) { }

    public new void Start()
    {
        base.Start();
        base.Connect(NetUtils.address, 9050, NetUtils.connectionKey);
    }

    public void Send(string message) =>
        SendToAll(
            new SeaStrikeNetDataWriter(message),
            DeliveryMethod.ReliableOrdered);
}