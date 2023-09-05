using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using Player = SeaStrike.PC.Root.Network.Player;

namespace SeaStrike.PC.Root.Screens;

public class MultiplayerBattlePhaseScreen : BattlePhaseScreen
{
    private Player player;

    public MultiplayerBattlePhaseScreen(
        Player player,
        SeaStrike game,
        Board playerBoard,
        Board opponentBoard) : base(game)
    {
        this.player = player;
        this.game = game;
        this.playerBoard = playerBoard;

        seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard);
    }

    protected override OpponentBattleGridPanel OpponentBattleGridPanel =>
        new MultiplayerOpponnentBattleGridPanel(
            player, game, seaStrikeGame, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };
}