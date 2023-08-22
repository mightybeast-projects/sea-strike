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

        Title = "Select ship properties : ";

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
            Text = "Selected position : " + position,
            Font = SeaStrike.fontSystem.GetFont(24),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddShipTypeSelectionForm()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Ship type : ",
            Font = SeaStrike.fontSystem.GetFont(24),
            GridRow = 1
        });

        shipsTypeBox = new ComboBox()
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        foreach (Ship ship in boardBuilder.shipsPool)
            shipsTypeBox.Items.Add(
                new ListItem(ship.GetType().Name +
                " (Width : " + ship.width + ")"));
        shipsTypeBox.SelectedIndex = 0;
        shipOptionsGrid.Widgets.Add(shipsTypeBox);
    }

    private void AddShipOrientationSelectionForm()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Ship orientation : ",
            Font = SeaStrike.fontSystem.GetFont(24),
            GridRow = 2,
        });

        shipOrientationBox = new ComboBox()
        {
            GridRow = 2,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        shipOrientationBox.Items.Add(new ListItem("Horizontal"));
        shipOrientationBox.Items.Add(new ListItem("Vertical"));
        shipOrientationBox.SelectedIndex = 0;

        shipOptionsGrid.Widgets.Add(shipOrientationBox);
    }

    private void AddCreateButton()
    {
        GameButton createButton = new GameButton()
        {
            Text = "Create new ship",
            GridRow = 3,
            GridColumnSpan = 2,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        createButton.TouchUp += (s, a) => AddNewShip();

        shipOptionsGrid.Widgets.Add(createButton);
    }

    private void AddNewShip()
    {
        Close();

        int shipTypeIndex = shipsTypeBox.SelectedIndex ?? 0;
        Ship ship = boardBuilder.shipsPool[shipTypeIndex];
        int shipOrientationIndex = shipOrientationBox.SelectedIndex ?? 0;
        Func<Ship, BoardBuilder> AddShip;

        if (shipOrientationIndex == 0)
            AddShip = boardBuilder.AddHorizontalShip;
        else
            AddShip = boardBuilder.AddVerticalShip;

        AddShip(ship).AtPosition(position);
    }
}