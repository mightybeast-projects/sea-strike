using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.GameCore.Root.Widgets.Modal;

public class ErrorWindow : GameWindow
{
    private string errorMessage;

    public ErrorWindow(string errorMessage)
        : base(SeaStrikeGame.stringStorage.errorWindowTitle)
    {
        this.errorMessage = errorMessage;

        TitleTextColor = Color.Red;
        Border = new SolidBrush(Color.Red);
        DragHandle = null;
        Content = ErrorLabel;
    }

    private Label ErrorLabel => new Label()
    {
        Text = errorMessage,
        Wrap = true
    };
}