using System.Text;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets;

public class HelpWindow : GameWindow
{
    private string labelText;

    public HelpWindow(string[] helpLabelContent)
    {
        Title = SeaStrike.stringStorage.helpWindowTitle;
        DragHandle = null;

        GetHelpText(helpLabelContent);

        Content = HelpLabel;
    }

    private void GetHelpText(string[] helpLabelContent)
    {
        StringBuilder builder = new StringBuilder();

        foreach (string str in helpLabelContent)
            builder.Append(str);

        labelText = builder.ToString();
    }

    private Label HelpLabel => new Label()
    {
        Text = labelText,
        Font = SeaStrike.fontSystem.GetFont(24)
    };
}