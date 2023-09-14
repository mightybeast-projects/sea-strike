using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets.Modal;

public class GameOverWindow : GameWindow
{
    private readonly SeaStrikeGame seaStrikeGame;

    public GameOverWindow(SeaStrikeGame seaStrikeGame, string title)
        : base(title)
    {
        this.seaStrikeGame = seaStrikeGame;

        DragHandle = null;
        CloseButton.Visible = false;

        Content = RestartButton;
    }

    private GameButton RestartButton => new GameButton(RestartGame)
    {
        Text = SeaStrikeGame.stringStorage.restartButtonLabel
    };

    private void RestartGame() =>
        seaStrikeGame.screenManager.LoadScreen(new MainMenuScreen(seaStrikeGame));
}