using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class ClearShipsButton : GameButton
{
    public ClearShipsButton(BoardBuilder boardBuilder)
    {
        Text = SeaStrike.stringStorage.clearButtonString;

        TouchUp += (s, a) => boardBuilder.ClearOceanGrid();
    }
}