using Microsoft.Xna.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;

namespace SeaStrike.PC.Root.Screens.Multiplayer;

public class MultiplayerBattlePhaseScreen : BattlePhaseScreen
{
    private NetPlayer player;

    public MultiplayerBattlePhaseScreen(NetPlayer player, Board opponentBoard)
        : base(player.game)
    {
        this.player = player;
        this.game = player.game;
        this.playerBoard = player.board;

        if (player.isHost)
            seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard);
        else
        {
            seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard, true);
            player.canShoot = false;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetwork();
    }

    protected override OpponentBattleGridPanel OpponentBattleGridPanel =>
        new MultiplayerOpponnentBattleGridPanel(
            player, seaStrikeGame, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };
}