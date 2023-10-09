using LiteNetLib.Utils;

namespace SeaStrike.GameCore.Root.Network;

public class SeaStrikeNetDataWriter : NetDataWriter
{
    public SeaStrikeNetDataWriter(string message) : base() => Put(message);
}