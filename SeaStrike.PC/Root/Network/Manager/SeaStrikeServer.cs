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
    }

    public new void Start() => base.Start(NetUtils.port);
}