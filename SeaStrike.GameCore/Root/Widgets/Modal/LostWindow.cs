using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;

namespace SeaStrike.GameCore.Root.Widgets.Modal;

public class LostWindow : GameOverWindow
{
    public LostWindow(Action onExitButtonClicked) :
        base(SeaStrikeGame.stringStorage.loseScreenTitle,
            onExitButtonClicked)
    {
        TitleTextColor = Color.Red;
        Border = new SolidBrush(Color.Red);

        SeaStrikeGame.audioManager.PlayLostOST();
    }
}