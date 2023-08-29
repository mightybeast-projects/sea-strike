using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets;

public class HelpWindow : GameWindow
{
    public HelpWindow(string helpLabelContent)
    {
        Title = "Help";

        VerticalStackPanel panel = new VerticalStackPanel();

        Label helpLabel = new Label()
        {
            Text = helpLabelContent,
            Font = SeaStrike.fontSystem.GetFont(24)
        };

        panel.Widgets.Add(helpLabel);

        Content = panel;
    }
}