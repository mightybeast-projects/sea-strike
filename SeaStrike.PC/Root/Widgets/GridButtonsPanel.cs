using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets.Button;

namespace SeaStrike.PC.Root.Widgets;

public class GridButtonsPanel : HorizontalStackPanel
{
    private readonly BoardBuilder boardBuilder;

    public GridButtonsPanel(BoardBuilder boardBuilder)
    {
        this.boardBuilder = boardBuilder;

        Top = 10;
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Top;
        GridRow = 1;
        Spacing = 50;

        Widgets.Add(RandomizeShipsButton);
        Widgets.Add(ClearShipsButton);
    }

    private GameButton RandomizeShipsButton =>
        new GameButton(() => boardBuilder.RandomizeShipsStartingPosition())
        {
            Text = SeaStrike.stringStorage.randomizeButtonString
        };

    private GameButton ClearShipsButton =>
        new GameButton(() => boardBuilder.ClearOceanGrid())
        {
            Text = SeaStrike.stringStorage.clearButtonString
        };
}