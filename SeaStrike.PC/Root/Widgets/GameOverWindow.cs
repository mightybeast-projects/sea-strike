using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class GameOverWindow : GameWindow
{
    public GameOverWindow(SeaStrike game)
    {
        DragHandle = null;
        CloseButton.Visible = false;

        TextButton restartButton = new GameButton()
        {
            Text = SeaStrike.stringStorage.restartButtonLabel
        };

        restartButton.TouchUp += (s, a) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));

        Content = restartButton;
    }
}