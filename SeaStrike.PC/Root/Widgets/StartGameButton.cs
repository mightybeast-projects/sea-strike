using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class StartGameButton : GameButton
{
    public StartGameButton(SeaStrike game, BoardBuilder boardBuilder)
    {
        Text = SeaStrike.stringStorage.startGameButtonLabel;

        TouchUp += (s, a) => game.screenManager.LoadScreen(
            new BattlePhaseScreen(game, boardBuilder.Build())
        );
    }
}