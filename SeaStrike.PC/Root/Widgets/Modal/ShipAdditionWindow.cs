using System;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets.Button;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Widgets.Modal;

public class ShipAdditionWindow : GameWindow
{
    private const string shipTypeBoxId = "shipType";
    private const string orientationBoxId = "orientationBox";
    private readonly string position;
    private readonly BoardBuilder boardBuilder;

    public ShipAdditionWindow(string position, BoardBuilder boardBuilder)
        : base(SeaStrike.stringStorage.shipAdditionWindowTitle)
    {
        this.position = position;
        this.boardBuilder = boardBuilder;

        Grid shipOptionsGrid = new Grid()
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };

        shipOptionsGrid.Widgets.Add(SelectedTileLabel);
        shipOptionsGrid.Widgets.Add(ShipTypeLabel);
        shipOptionsGrid.Widgets.Add(ShipTypeComboBox);
        shipOptionsGrid.Widgets.Add(ShipOrientationLabel);
        shipOptionsGrid.Widgets.Add(ShipOrientationComboBox);
        shipOptionsGrid.Widgets.Add(CreateShipButton);

        Content = shipOptionsGrid;
    }

    private Label SelectedTileLabel => new Label()
    {
        Text = SeaStrike.stringStorage.selectedPositionLabel + position,
        HorizontalAlignment = HorizontalAlignment.Center,
        GridColumnSpan = 2
    };

    private Label ShipTypeLabel => new Label()
    {
        Text = SeaStrike.stringStorage.shipTypeLabel,
        GridRow = 1
    };

    private GameComboBox ShipTypeComboBox => new GameComboBox(GetShipTypes())
    {
        Id = shipTypeBoxId,
        GridRow = 1,
        GridColumn = 1,
        HorizontalAlignment = HorizontalAlignment.Right
    };

    private Label ShipOrientationLabel => new Label()
    {
        Text = SeaStrike.stringStorage.shipOrientationLabel,
        GridRow = 2,
    };

    private GameComboBox ShipOrientationComboBox =>
        new GameComboBox(SeaStrike.stringStorage.orientationBoxItems)
        {
            Id = orientationBoxId,
            GridRow = 2,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right
        };

    private GameButton CreateShipButton => new GameButton(AddNewShip)
    {
        Text = SeaStrike.stringStorage.createShipButtonLabel,
        GridRow = 3,
        GridColumnSpan = 2,
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private void AddNewShip()
    {
        Close();

        ComboBox shipsTypeBox =
            (ComboBox)Content.FindWidgetById(shipTypeBoxId);
        ComboBox shipOrientationBox =
            (ComboBox)Content.FindWidgetById(orientationBoxId);

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