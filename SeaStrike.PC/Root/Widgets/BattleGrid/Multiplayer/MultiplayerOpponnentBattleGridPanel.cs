using System.Linq;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.GridTile;

namespace SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;

public class MultiplayerOpponnentBattleGridPanel : OpponentBattleGridPanel
{
    private NetPlayer player;

    public MultiplayerOpponnentBattleGridPanel(
        NetPlayer player,
        SeaStrikeGame seaStrikeGame,
        Board opponentBoard)
        : base(player.game, seaStrikeGame, opponentBoard) =>
            this.player = player;

    protected override void ShootTile(object sender)
    {
        if (player.canShoot)
        {
            string tileStr = ((EmptyGridTileButton)sender).tile.notation;

            ShotResult result = seaStrikeGame.HandleCurrentPlayerShot(tileStr);

            ((Label)Widgets.Last()).Text = result.ToString();

            if (seaStrikeGame.isOver)
                player.game.ShowVictoryScreen();

            player.SendShotTile(result.tile);

            player.canShoot = false;
        }
    }
}