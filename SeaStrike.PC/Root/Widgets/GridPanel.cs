using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets.GridTile;
using Grid = Myra.Graphics2D.UI.Grid;
using OceanGrid = SeaStrike.Core.Entity.Grid;

namespace SeaStrike.PC.Root.Widgets;

public class GridPanel : Panel, IBoardObserver
{
    public Action<object> OnEmptyTileClicked
    {
        private get => onEmptyTileClicked;
        set
        {
            onEmptyTileClicked = value;
            UpdateContent();
        }
    }
    public Action<object> OnAllyShipClicked
    {
        private get => onAllyShipClicked;
        set
        {
            onAllyShipClicked = value;
            UpdateContent();
        }
    }

    private readonly OceanGrid oceanGrid;
    private readonly bool showShips;
    private Grid mainGrid;
    private Action<object> onEmptyTileClicked;
    private Action<object> onAllyShipClicked;

    public GridPanel(Board playerBoard, bool showShips)
    {
        this.showShips = showShips;
        oceanGrid = playerBoard.oceanGrid;

        Width = 343;
        Height = 343;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        playerBoard.Subscribe(this);
    }

    public void Notify() => UpdateContent();

    private void UpdateContent()
    {
        Widgets.Clear();

        mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Green
        };

        AddGridProportions();

        AddBoardGridLabels();
        AddGridTiles();

        Widgets.Add(mainGrid);
    }

    private void AddGridProportions()
    {
        for (int i = 0; i < oceanGrid.width + 1; i++)
            mainGrid.RowsProportions.Add(WidthProportion);

        for (int i = 0; i < oceanGrid.height + 1; i++)
            mainGrid.ColumnsProportions.Add(HeightProportion);
    }

    private void AddBoardGridLabels()
    {
        for (int i = 1; i < oceanGrid.width + 1; i++)
            mainGrid.Widgets.Add(NumberLabel(i));

        for (int i = 1; i < oceanGrid.height + 1; i++)
            mainGrid.Widgets.Add(LetterLabel(i));
    }

    private void AddGridTiles()
    {
        for (int i = 1; i < oceanGrid.width + 1; i++)
            for (int j = 1; j < oceanGrid.height + 1; j++)
                mainGrid.Widgets.Add(GridTile(oceanGrid.tiles[i - 1, j - 1]));
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

    private Func<int, Label> NumberLabel = (int i) => new Label()
    {
        Text = i.ToString(),
        GridColumn = i,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
    };

    private Func<int, Label> LetterLabel = (int i) => new Label()
    {
        Text = ((char)(i + 64)).ToString(),
        GridRow = i,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
    };

    private Widget GridTile(Tile tile)
    {
        if (!tile.isOccupied && tile.hasBeenHit)
            return new MissGridTileImage(tile);
        else if (tile.isOccupied && tile.hasBeenHit)
            return new EnemyShipGridTileImage(tile);
        else if (tile.isOccupied && !tile.hasBeenHit && showShips)
            return new AllyShipGridTileButton(tile, onAllyShipClicked);
        else
            return new EmptyGridTileButton(tile, onEmptyTileClicked);
    }
}