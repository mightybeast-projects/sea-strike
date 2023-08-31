using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Widgets.Button;

namespace SeaStrike.PC.Root.Widgets;

public class GameOverWindow : GameWindow
{
    private readonly SeaStrike game;

    public GameOverWindow(SeaStrike game, string title) : base(title)
    {
        this.game = game;

        DragHandle = null;
        CloseButton.Visible = false;

        Content = RestartButton;
    }

    private GameButton RestartButton => new GameButton(RestartGame)
    {
        Text = SeaStrike.stringStorage.restartButtonLabel
    };

    private void RestartGame() =>
        game.screenManager.LoadScreen(new MainMenuScreen(game));
}