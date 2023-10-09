using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets.Modal;

public class ReadyWindow : GameWindow
{
    public ReadyWindow() : base(SeaStrikeGame.stringStorage.readyWindowTitle)
    {
        DragHandle = null;
        CloseButton.Visible = false;

        Content = ContentLabel;
    }

    private Label ContentLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.readyWindowContentLabel
    };
}