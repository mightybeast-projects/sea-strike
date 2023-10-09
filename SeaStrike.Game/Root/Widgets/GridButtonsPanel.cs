using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class GridButtonsPanel : HorizontalStackPanel
{
    private readonly BoardBuilder boardBuilder;

    public GridButtonsPanel(BoardBuilder boardBuilder)
    {
        this.boardBuilder = boardBuilder;

        Spacing = 50;

        Widgets.Add(RandomizeShipsButton);
        Widgets.Add(ClearShipsButton);
    }

    private GameButton RandomizeShipsButton =>
        new GameButton(() => boardBuilder.RandomizeShipsStartingPosition())
        {
            Text = SeaStrikeGame.stringStorage.randomizeButtonString
        };

    private GameButton ClearShipsButton =>
        new GameButton(() => boardBuilder.ClearOceanGrid())
        {
            Text = SeaStrikeGame.stringStorage.clearButtonString
        };
}