using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets;

public class GameComboBox : ComboBox
{
    public GameComboBox(string[] items)
    {
        foreach (string str in items)
            Items.Add(new ListItem(str));

        SelectedIndex = 0;
    }
}