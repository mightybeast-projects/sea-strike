using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.GridTile;

namespace SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;

public class NetOpponnentBattleGridPanel : OpponentBattleGridPanel
{
    private NetPlayer player;

    public NetOpponnentBattleGridPanel(
        NetPlayer player,
        SeaStrikeGame seaStrikeGame,
        Board opponentBoard)
        : base(player.game, seaStrikeGame, opponentBoard) =>
            this.player = player;

    protected override void ShootTile(object sender)
    {
        if (player.canShoot)
            MakeShot(((EmptyGridTileButton)sender).tile);
    }

    private void MakeShot(Tile tile)
    {
        ShotResult result = seaStrikeGame.HandleCurrentPlayerShot(tile.notation);

        shotResultLabel.Text = result.ToString();

        if (seaStrikeGame.isOver)
            player.game.ShowVictoryScreen();

        player.SendShotTile(result.tile);
    }
}