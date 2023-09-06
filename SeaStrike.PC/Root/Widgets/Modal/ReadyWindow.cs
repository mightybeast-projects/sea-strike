using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets.Modal;

public class ReadyWindow : GameWindow
{
    public ReadyWindow() : base(SeaStrike.stringStorage.readyWindowTitle)
    {
        DragHandle = null;
        CloseButton.Visible = false;

        Content = ContentLabel;
    }

    private Label ContentLabel => new Label()
    {
        Text = SeaStrike.stringStorage.readyWindowContentLabel
    };
}