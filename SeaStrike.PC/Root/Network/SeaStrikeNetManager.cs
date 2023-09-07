using LiteNetLib;
using LiteNetLib.Utils;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeNetManager
{
    protected NetManager netManager;

    protected NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }
}