using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class GridButtonsPanel : HorizontalStackPanel
{
    public GridButtonsPanel(BoardBuilder boardBuilder)
    {
        Top = 10;
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Top;
        GridRow = 1;
        Spacing = 50;

        Widgets.Add(new RandomizeShipsButton(boardBuilder));

        TextButton clearButton = new GameButton()
        {
            Text = SeaStrike.stringStorage.clearButtonString
        };
        clearButton.TouchUp += (s, a) => boardBuilder.ClearOceanGrid();

        Widgets.Add(clearButton);
    }
}