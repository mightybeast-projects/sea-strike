using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic.Utility;
using SeaStrike.GameCore.Root.Network;
using SeaStrike.GameCore.Root.Widgets.GridTile;

namespace SeaStrike.GameCore.Root.Widgets.BattleGrid.Multiplayer;

public class NetOpponnentBattleGridPanel : OpponentBattleGridPanel
{
    private new NetPlayer player;

    public NetOpponnentBattleGridPanel(NetPlayer player) : base(player) =>
        this.player = player;

    protected override void ShootTile(object sender)
    {
        if (player.canShoot)
            MakeShot(((EmptyGridTileButton)sender).tile);
    }

    private void MakeShot(Tile tile)
    {
        ShotResult result = game.HandleCurrentPlayerShot(tile.notation);

        shotResultLabel.Text = result.ToString();

        SeaStrikeGame.audioManager.PlayShotResultSFX(result);

        player.SendShotTile(result.tile);

        if (game.isOver)
            player.ShowVictoryScreen();
    }
}