using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.GameCore.Root.Network;

namespace SeaStrike.GameCore.Root.Widgets;

public class CurrentPlayerTurnLabel : Label
{
    private NetPlayer player;

    public CurrentPlayerTurnLabel(NetPlayer player)
    {
        this.player = player;

        Font = SeaStrikeGame.fontManager.GetFont(28);
    }

    public void Update()
    {
        if (player.canShoot)
            ShowThatPlayerCanShoot();
        else
            ShowThatPlayerCannotShoot();
    }

    private void ShowThatPlayerCanShoot()
    {
        Text = SeaStrikeGame.stringStorage.yourTurnLabel;
        TextColor = Color.LawnGreen;
    }

    private void ShowThatPlayerCannotShoot()
    {
        Text = SeaStrikeGame.stringStorage.opponentsTurnLabel;
        TextColor = Color.Red;
    }
}