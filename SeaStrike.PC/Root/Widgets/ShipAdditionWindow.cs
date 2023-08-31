using System;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Widgets;

public class ShipAdditionWindow : GameWindow
{
    private readonly string position;
    private readonly BoardBuilder boardBuilder;
    private Grid shipOptionsGrid;
    private ComboBox shipsTypeBox;
    private ComboBox shipOrientationBox;

    public ShipAdditionWindow(string position, BoardBuilder boardBuilder)
    {
        this.position = position;
        this.boardBuilder = boardBuilder;

        Title = SeaStrike.stringStorage.shipAdditionWindowTitle;

        shipOptionsGrid = new Grid()
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };

        AddSelectedTileLabel();
        AddShipTypeSelectionForm();
        AddShipOrientationSelectionForm();
        AddCreateButton();

        Content = shipOptionsGrid;
    }

    private void AddSelectedTileLabel()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = SeaStrike.stringStorage.selectedPositionLabel + position,
            Font = SeaStrike.fontSystem.GetFont(24),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddShipTypeSelectionForm()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = SeaStrike.stringStorage.shipTypeLabel,
            Font = SeaStrike.fontSystem.GetFont(24),
            GridRow = 1
        });

        shipsTypeBox = new GameComboBox(GetShipTypes())
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right
        };

        shipOptionsGrid.Widgets.Add(shipsTypeBox);
    }

    private void AddShipOrientationSelectionForm()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = SeaStrike.stringStorage.shipOrientationLabel,
            Font = SeaStrike.fontSystem.GetFont(24),
            GridRow = 2,
        });

        shipOrientationBox =
            new GameComboBox(SeaStrike.stringStorage.orientationBoxItems)
            {
                GridRow = 2,
                GridColumn = 1,
                HorizontalAlignment = HorizontalAlignment.Right
            };

        shipOptionsGrid.Widgets.Add(shipOrientationBox);
    }

    private void AddCreateButton()
    {
        shipOptionsGrid.Widgets.Add(new CreateShipButton(AddNewShip)
        {
            GridRow = 3,
            GridColumnSpan = 2,
            HorizontalAlignment = HorizontalAlignment.Center
        });
    }

    private void AddNewShip()
    {
        Close();

        int shipTypeIndex = shipsTypeBox.SelectedIndex ?? 0;
        Ship ship = boardBuilder.shipsPool[shipTypeIndex];
        int shipOrientationIndex = shipOrientationBox.SelectedIndex ?? 0;
        Func<Ship, BoardBuilder> AddShip =
            shipOrientationIndex == 0 ?
            boardBuilder.AddHorizontalShip :
            boardBuilder.AddVerticalShip;

        AddShip(ship).AtPosition(position);
    }

    private string[] GetShipTypes()
    {
        string[] ships = new string[boardBuilder.shipsPool.Length];

        for (int i = 0; i < boardBuilder.shipsPool.Length; i++)
        {
            Ship ship = boardBuilder.shipsPool[i];
            ships[i] = ship.GetType().Name +
                        " (" +
                        SeaStrike.stringStorage.shipWidthLabel +
                        ship.width +
                        ")";
        }

        return ships;
    }
}