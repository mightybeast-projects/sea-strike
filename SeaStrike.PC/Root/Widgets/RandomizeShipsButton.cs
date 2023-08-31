using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class RandomizeShipsButton : GameButton
{
    public RandomizeShipsButton(BoardBuilder boardBuilder)
    {
        Text = SeaStrike.stringStorage.randomizeButtonString;

        TouchUp += (s, a) => boardBuilder.RandomizeShipsStartingPosition();
    }
}