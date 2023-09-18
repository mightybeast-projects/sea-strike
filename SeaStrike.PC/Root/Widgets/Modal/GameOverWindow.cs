using SeaStrike.PC.Root.Screens;

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

        Content = ExitButton;
    }

    private GameButton ExitButton =>
        new GameButton(player.RedirectTo<MainMenuScreen>)
        {
            Text = SeaStrikeGame.stringStorage.exitButtonLabel
        };
}