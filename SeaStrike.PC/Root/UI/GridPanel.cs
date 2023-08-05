using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using SeaStrikeGrid = SeaStrike.Core.Entity.Grid;

namespace SeaStrike.PC.Root.UI;

public class GridPanel : Panel
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGrid seaStrikeGrid;
    private readonly Grid grid;

    public GridPanel(SeaStrike game, SeaStrikeGrid seaStrikeGrid)
    {
        Width = 343;
        Height = 343;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        this.game = game;
        this.seaStrikeGrid = seaStrikeGrid;

        grid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Green
        };

        AddGridProportions();
        AddBoardGridLabels();

        Widgets.Add(grid);
    }

    private void AddGridProportions()
    {
        for (int i = 0; i < 11; i++)
        {
            grid.RowsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = (float)Width / 11
            });
            grid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = (float)Height / 11
            });
        }
    }

    private void AddBoardGridLabels()
    {
        for (int i = 1; i < 11; i++)
        {
            AddNumberLabel(i);
            AddLetterLabel(i);
        }
    }

    private void AddNumberLabel(int i)
    {
        grid.Widgets.Add(new Label()
        {
            Text = i.ToString(),
            Font = game.fontSystem.GetFont(24),
            GridColumn = i,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddLetterLabel(int i)
    {
        grid.Widgets.Add(new Label()
        {
            Text = ((char)(i + 64)).ToString(),
            Font = game.fontSystem.GetFont(24),
            GridRow = i,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }
}