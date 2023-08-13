using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using SeaStrike.Core.Entity;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.UI;

public class ShipAdditionDialog : Dialog
{
    private readonly string position;
    private readonly List<Ship> shipPool;
    private readonly BoardBuilder boardBuilder;
    private Grid shipOptionsGrid;
    private ComboBox shipsTypeBox;
    private ComboBox shipOrientationBox;

    public ShipAdditionDialog(TextButton emptyTileButton, List<Ship> shipPool, BoardBuilder boardBuilder)
    {
        position = emptyTileButton.Text;
        this.shipPool = shipPool;
        this.boardBuilder = boardBuilder;

        shipOptionsGrid = new Grid()
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };

        AddSelectedTileLabel();
        AddShipTypeSelectionForm();
        AddShipOrientationSelectionForm();

        Content = shipOptionsGrid;

        SetDialogProperties();
    }

    private void AddSelectedTileLabel()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Selected position : " + position,
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddShipTypeSelectionForm()
    {
        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Ship type : ",
            GridRow = 1
        });

        shipsTypeBox = new ComboBox()
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right,
        };
        foreach (Ship ship in shipPool)
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

    private void SetDialogProperties()
    {
        Title = "Select ship properties";
        Background = new SolidBrush(Color.Black);
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);
        ButtonOk.Text = "Create new ship";
        ButtonOk.TouchDown += (s, a) => AddNewShip();
        ButtonCancel.Visible = false;
        ConfirmKey = Keys.Enter;
        CloseKey = Keys.Escape;
    }

    private void AddNewShip()
    {
        int shipTypeIndex = shipsTypeBox.SelectedIndex ?? 0;
        Ship ship = shipPool[shipTypeIndex];
        int shipOrientationIndex = shipOrientationBox.SelectedIndex ?? 0;

        Func<Ship, BoardBuilder> additionMethod;

        if (shipOrientationIndex == 0)
            additionMethod = boardBuilder.AddHorizontalShip;
        else
            additionMethod = boardBuilder.AddVerticalShip;

        try
        {
            additionMethod(ship).AtPosition(position);
            shipPool.Remove(ship);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }
}