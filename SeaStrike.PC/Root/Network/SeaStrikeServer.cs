using LiteNetLib;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer : SeaStrikeNetManager
{
    public SeaStrikeServer(NetPlayer player)
    {
        player.server = this;

        var listener = new SeaStrikeServerListener(player);
        netManager = new NetManager(listener);

        listener.server = netManager;
    }

    public void Start() => netManager.Start(NetUtils.port);

    public void Disconnect()
    {
        netManager.DisconnectAll();
        netManager.Stop();
    }

    public void PollEvents() => netManager.PollEvents();
}