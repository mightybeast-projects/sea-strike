using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using Grid = Myra.Graphics2D.UI.Grid;
using OceanGrid = SeaStrike.Core.Entity.Grid;

namespace SeaStrike.PC.Root.UI;

public class GridPanel : Panel
{
    private readonly SeaStrike game;
    private readonly OceanGrid oceanGrid;
    private readonly Grid uiGrid;

    public GridPanel(SeaStrike game, OceanGrid oceanGrid)
    {
        Width = 343;
        Height = 343;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        this.game = game;
        this.oceanGrid = oceanGrid;

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
            uiGrid.RowsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = (float)Width / (oceanGrid.width + 1)
            });
            uiGrid.ColumnsProportions.Add(new Proportion()
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
                Tile tile = oceanGrid.tiles[i - 1, j - 1];

                if (!tile.isOccupied && tile.hasBeenHit)
                    AddHitTile(tile);
                else if (tile.isOccupied && tile.hasBeenHit)
                    AddHitShipTile(tile);
                else if (tile.isOccupied && !tile.hasBeenHit)
                    AddShipTile(tile);
                else
                    AddEmptyTile(tile);
            }
        }
    }

    private void AddNumberLabel(int i)
    {
        uiGrid.Widgets.Add(new Label()
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
        uiGrid.Widgets.Add(new Label()
        {
            Text = ((char)(i + 64)).ToString(),
            Font = game.fontSystem.GetFont(24),
            GridRow = i,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddEmptyTile(Tile tile)
    {
        uiGrid.Widgets.Add(new Label()
        {
            Text = tile.notation,
            Opacity = 0.1f,
            Font = game.fontSystem.GetFont(24),
            GridColumn = tile.i + 1,
            GridRow = tile.j + 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddHitTile(Tile tile)
    {
        Texture2D miss = new Texture2D(game.GraphicsDevice, 1, 1);
        miss.SetData(new[] { Color.White });

        uiGrid.Widgets.Add(new Image()
        {
            Renderable = new TextureRegion(miss, new Rectangle(0, 0, 15, 15)),
            GridColumn = tile.i + 1,
            GridRow = tile.j + 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddShipTile(Tile tile)
    {
        Texture2D ship = new Texture2D(game.GraphicsDevice, 1, 1);
        ship.SetData(new[] { Color.LawnGreen });

        uiGrid.Widgets.Add(new Image()
        {
            Renderable = new TextureRegion(ship, new Rectangle(0, 0, 15, 15)),
            GridColumn = tile.i + 1,
            GridRow = tile.j + 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }

    private void AddHitShipTile(Tile tile)
    {
        Texture2D damagedShip = new Texture2D(game.GraphicsDevice, 1, 1);
        damagedShip.SetData(new[] { Color.Red });

        uiGrid.Widgets.Add(new Image()
        {
            Renderable = new TextureRegion(damagedShip, new Rectangle(0, 0, 15, 15)),
            GridColumn = tile.i + 1,
            GridRow = tile.j + 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        });
    }
}