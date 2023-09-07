using LiteNetLib;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeClient : SeaStrikeNetManager
{
    public SeaStrikeClient(NetPlayer player)
    {
        player.client = this;

        netManager = new NetManager(new SeaStrikeClientListener(player));
    }

    public void Start()
    {
        netManager.Start();
        netManager.Connect(NetUtils.address, 9050, NetUtils.connectionKey);
    }

    public void Disconnect() => netManager.DisconnectAll();

    public void PollEvents() => netManager.PollEvents();

    public void Send(string message) =>
        netManager.SendToAll(
            FormMessage(message), DeliveryMethod.ReliableOrdered);
}