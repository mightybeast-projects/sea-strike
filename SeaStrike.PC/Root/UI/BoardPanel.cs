using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.UI;

public class BoardPanel : Panel
{
    private readonly SeaStrike game;
    private readonly Grid grid;

    public BoardPanel(SeaStrike game)
    {
        Width = 343;
        Height = 343;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        this.game = game;

        grid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Green
        };

        AddGridProportions();
        AddBoardGridLabels();

        AddChild(grid);
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
            grid.Widgets.Add(new Label()
            {
                Text = i.ToString(),
                Font = game.fontSystem.GetFont(24),
                GridColumn = i,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });

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
}