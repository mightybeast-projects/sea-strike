using LiteNetLib;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeServer : NetManager
{
    public SeaStrikeServer(SeaStrikeServerListener listener) : base(listener)
        => listener.server = this;

    public new void Start() => base.Start(NetUtils.port);
}