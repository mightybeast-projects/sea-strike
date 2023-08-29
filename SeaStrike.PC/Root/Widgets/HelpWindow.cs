using System.Text;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets;

public class HelpWindow : GameWindow
{
    public HelpWindow(string[] helpLabelContent)
    {
        Title = SeaStrike.stringStorage.helpWindowTitle;

        StringBuilder builder = new StringBuilder();
        foreach (string str in helpLabelContent)
            builder.Append(str);

        string labelText = builder.ToString();

        VerticalStackPanel panel = new VerticalStackPanel();

        Label helpLabel = new Label()
        {
            Text = labelText,
            Font = SeaStrike.fontSystem.GetFont(24)
        };

        panel.Widgets.Add(helpLabel);

        Content = panel;
        DragHandle = null;
    }
}