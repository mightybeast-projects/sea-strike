using Microsoft.Xna.Framework;

namespace SeaStrike.GameCore.Root.Widgets.Modal;

public class VictoryWindow : GameOverWindow
{
    public VictoryWindow(Action onExitButtonClicked) :
        base(SeaStrikeGame.stringStorage.victoryScreenTitle,
            onExitButtonClicked)
    {
        TitleTextColor = Color.LawnGreen;

        SeaStrikeGame.audioManager.PlayVictoryOST();
    }
}