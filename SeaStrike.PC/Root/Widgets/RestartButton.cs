using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class RestartButton : GameButton
{
    public RestartButton(SeaStrike game)
    {
        Text = SeaStrike.stringStorage.restartButtonLabel;

        TouchUp += (s, a) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));
    }
}