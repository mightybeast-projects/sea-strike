namespace SeaStrike.GameCore.Root.Widgets.Modal;

public abstract class GameOverWindow : GameWindow
{
    private readonly Action onExitButtonClicked;

    public GameOverWindow(string title, Action onExitButtonClicked)
        : base(title)
    {
        this.onExitButtonClicked = onExitButtonClicked;

        DragHandle = null;
        CloseButton.Visible = false;

        Content = ExitButton;
    }

    private GameButton ExitButton => new GameButton(onExitButtonClicked)
    {
        Text = SeaStrikeGame.stringStorage.exitButtonLabel
    };
}