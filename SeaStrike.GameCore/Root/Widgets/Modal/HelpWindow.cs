using FontStashSharp.RichText;
using Myra.Graphics2D.UI;
using System.Text;

namespace SeaStrike.GameCore.Root.Widgets.Modal;

public class HelpWindow : GameWindow
{
    private string labelText;

    public HelpWindow(string[] helpLabelContent) :
        base(SeaStrikeGame.stringStorage.helpWindowTitle)
    {
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
        TextAlign = TextHorizontalAlignment.Center
    };
}