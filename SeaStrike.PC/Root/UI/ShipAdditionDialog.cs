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
    private Ship[] shipPool = new Ship[]
    {
        new Destroyer(),
        new Cruiser(),
        new Submarine(),
        new Battleship(),
        new Carrier()
    };

    public ShipAdditionDialog(SeaStrike game, TextButton emptyTileButton)
    {
        Grid shipOptionsGrid = new Grid()
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };

        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Selected position : " + emptyTileButton.Text,
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });

        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Ship type : ",
            GridRow = 1
        });

        ComboBox shipsTypeBox = new ComboBox()
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

        shipOptionsGrid.Widgets.Add(new Label()
        {
            Text = "Ship orientation : ",
            GridRow = 2,
        });

        ComboBox shipOrientationBox = new ComboBox()
        {
            GridRow = 2,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        shipOrientationBox.Items.Add(new ListItem("Horizontal"));
        shipOrientationBox.Items.Add(new ListItem("Vertical"));
        shipOrientationBox.SelectedIndex = 0;
        shipOptionsGrid.Widgets.Add(shipOrientationBox);

        Title = "Select ship properties";
        Background = new SolidBrush(Color.Black);
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);
        Content = shipOptionsGrid;
        ButtonOk.Text = "Create new ship";
        ButtonCancel.Visible = false;
        ConfirmKey = Keys.Enter;
        CloseKey = Keys.Escape;
    }
}