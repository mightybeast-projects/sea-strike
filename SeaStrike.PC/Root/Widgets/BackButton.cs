using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class BackButton : GameButton
{
    public BackButton(SeaStrike game)
    {
        Text = SeaStrike.stringStorage.backButtonLabel;
        Width = 40;
        Height = 40;

        TouchUp += (s, a) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));
    }
}