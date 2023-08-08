using Microsoft.Xna.Framework;
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
        Grid optionsGrid = new Grid()
        {
            RowSpacing = 8
        };

        optionsGrid.Widgets.Add(new Label()
        {
            Text = "Selected position : " + emptyTileButton.Text,
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
        optionsGrid.Widgets.Add(new Label()
        {
            Text = "Select ship to add : ",
            GridRow = 1
        });

        ComboBox shipsType = new ComboBox()
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right,
        };
        foreach (Ship ship in shipPool)
            shipsType.Items.Add(
                new ListItem(ship.GetType().Name +
                " (Width : " + ship.width + ")"));
        shipsType.SelectedIndex = 0;
        optionsGrid.Widgets.Add(shipsType);

        Title = "Select ship properties";
        FocusedBorder = new SolidBrush(Color.LawnGreen);
        Content = optionsGrid;
    }
}