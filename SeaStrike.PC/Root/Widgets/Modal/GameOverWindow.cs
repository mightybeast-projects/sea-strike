namespace SeaStrike.PC.Root.Widgets.Modal;

public class GameOverWindow : GameWindow
{
    private readonly SeaStrikePlayer player;

    public GameOverWindow(SeaStrikePlayer player, string title)
        : base(title)
    {
        this.player = player;

        DragHandle = null;
        CloseButton.Visible = false;

        Content = RestartButton;
    }

    private GameButton RestartButton =>
        new GameButton(player.RedirectToMainMenuScreen)
        {
            Text = SeaStrikeGame.stringStorage.restartButtonLabel
        };
}