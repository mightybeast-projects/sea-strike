using LiteNetLib.Utils;

namespace SeaStrike.PC.Root.Network;

public class SeaStrikeNetDataWriter : NetDataWriter
{
    public SeaStrikeNetDataWriter(string message) : base() => Put(message);
}