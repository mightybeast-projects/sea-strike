using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic.Utility;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.GridTile;

namespace SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;

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

        if (game.isOver)
            player.ShowVictoryScreen();

        player.SendShotTile(result.tile);
    }
}