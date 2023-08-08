using Myra.Graphics2D.UI;
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
            ShowGridLines = true
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
            HorizontalAlignment = HorizontalAlignment.Right
        };
        foreach (Ship ship in shipPool)
            shipsType.Items.Add(new ListItem(ship.GetType().Name));
        shipsType.SelectedIndex = 0;
        optionsGrid.Widgets.Add(shipsType);

        Title = "Select ship properties";
        Content = optionsGrid;
    }
}