using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using Grid = Myra.Graphics2D.UI.Grid;
using OceanGrid = SeaStrike.Core.Entity.Grid;

namespace SeaStrike.PC.Root.Widgets;

public class GridPanel : Panel, IBoardObserver
{
    public Action<object> OnEmptyTileClicked;
    public Action<object> OnOccupiedTileClicked;

    private readonly OceanGrid oceanGrid;
    private readonly bool showShips;
    private Grid uiGrid;

    public GridPanel(OceanGrid oceanGrid, bool showShips)
    {
        this.oceanGrid = oceanGrid;
        this.showShips = showShips;

        Width = 343;
        Height = 343;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        UpdateContent();
    }

    public void Notify() => UpdateContent();

    private void UpdateContent()
    {
        Widgets.Clear();

        uiGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Green
        };

        AddGridProportions();
        AddBoardGridLabels();
        AddGridTiles();

        Widgets.Add(uiGrid);
    }

    private void AddGridProportions()
    {
        for (int i = 0; i < oceanGrid.width + 1; i++)
        {
            uiGrid.RowsProportions.Add(WidthProportion);
            uiGrid.ColumnsProportions.Add(HeightProportion);
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
            for (int j = 1; j < oceanGrid.height + 1; j++)
                AddGridTile(oceanGrid.tiles[i - 1, j - 1]);
    }

    private void AddNumberLabel(int i)
    {
        uiGrid.Widgets.Add(new Label()
        {
            Text = i.ToString(),
            Font = SeaStrike.fontSystem.GetFont(24),
            GridColumn = i,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddLetterLabel(int i)
    {
        uiGrid.Widgets.Add(new Label()
        {
            Text = ((char)(i + 64)).ToString(),
            Font = SeaStrike.fontSystem.GetFont(24),
            GridRow = i,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddGridTile(Tile tile)
    {
        if ((showShips && tile.isOccupied) || tile.hasBeenHit)
            AddAllyGridTileImage(tile);
        else
            AddEmptyGridTileButton(tile);
    }

    private void AddAllyGridTileImage(Tile tile)
    {
        GridTileImageButton tileButton = new GridTileImageButton(tile);
        if (tile.isOccupied)
            tileButton.TouchUp += (s, a) => OnOccupiedTileClicked?.Invoke(s);

        uiGrid.Widgets.Add(tileButton);
    }

    private void AddEmptyGridTileButton(Tile tile)
    {
        TextButton tileButton = new TextButton()
        {
            Text = tile.notation,
            Opacity = 0.1f,
            Font = SeaStrike.fontSystem.GetFont(18),
            GridColumn = tile.i + 1,
            GridRow = tile.j + 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        tileButton.TouchUp += (s, a) => OnEmptyTileClicked?.Invoke(s);

        uiGrid.Widgets.Add(tileButton);
    }

    private Proportion WidthProportion => new Proportion()
    {
        Type = ProportionType.Pixels,
        Value = (float)Width / (oceanGrid.width + 1)
    };

    private Proportion HeightProportion => new Proportion()
    {
        Type = ProportionType.Pixels,
        Value = (float)Height / (oceanGrid.height + 1)
    };
}