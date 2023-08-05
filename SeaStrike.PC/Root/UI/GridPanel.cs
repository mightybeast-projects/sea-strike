using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using OceanGrid = SeaStrike.Core.Entity.Grid;

namespace SeaStrike.PC.Root.UI;

public class GridPanel : Panel
{
    private readonly SeaStrike game;
    private readonly OceanGrid oceanGrid;
    private readonly Grid grid;

    public GridPanel(SeaStrike game, OceanGrid oceanGrid)
    {
        Width = 343;
        Height = 343;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        this.game = game;
        this.oceanGrid = oceanGrid;

        grid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Green
        };

        AddGridProportions();
        AddBoardGridLabels();
        AddGridTiles();

        Widgets.Add(grid);
    }

    private void AddGridProportions()
    {
        for (int i = 0; i < oceanGrid.width + 1; i++)
        {
            grid.RowsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = (float)Width / (oceanGrid.width + 1)
            });
            grid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = (float)Height / (oceanGrid.height + 1)
            });
        }
    }

    private void AddBoardGridLabels()
    {
        for (int i = 1; i < oceanGrid.width + 1; i++)
        {
            AddNumberLabel(i);
            AddLetterLabel(i);
        }
    }

    private void AddGridTiles()
    {
        for (int i = 1; i < oceanGrid.width + 1; i++)
        {
            for (int j = 1; j < oceanGrid.height + 1; j++)
            {
                grid.Widgets.Add(new Label()
                {
                    Text = oceanGrid.tiles[i - 1, j - 1].notation,
                    Opacity = 0.1f,
                    Font = game.fontSystem.GetFont(24),
                    GridColumn = i,
                    GridRow = j,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                });
            }
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