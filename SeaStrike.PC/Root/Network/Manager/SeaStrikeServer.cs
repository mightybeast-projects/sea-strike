using LiteNetLib;
using SeaStrike.PC.Root.Network.Listener;

namespace SeaStrike.PC.Root.Network.Manager;

public class SeaStrikeServer : NetManager
{
    public SeaStrikeServer(SeaStrikeServerListener listener) : base(listener)
    {
        listener.server = this;

        BroadcastReceiveEnabled = true;
        DisconnectTimeout = 3000;
        IPv6Enabled = false;
        ReuseAddress = true;
    }

    public new void Start()
    {
        if (!IsRunning)
            base.Start(NetUtils.port);
    }
}