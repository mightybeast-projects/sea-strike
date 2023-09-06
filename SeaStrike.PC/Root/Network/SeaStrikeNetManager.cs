using LiteNetLib;
using LiteNetLib.Utils;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeNetManager
{
    protected NetPlayer player;
    protected NetManager netManager;

    protected SeaStrikeNetManager(NetPlayer player) => this.player = player;

    protected NetDataWriter FormMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);

        return writer;
    }

    protected bool MessageIsBoardData(string message) =>
        message.StartsWith('{') && message.EndsWith('}');
}