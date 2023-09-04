using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.PC.Root.Screens;

public class MultiplayerBattlePhaseScreen : BattlePhaseScreen
{
    public MultiplayerBattlePhaseScreen(
        SeaStrike game,
        Board playerBoard,
        Board opponentBoard) : base(game)
    {
        this.game = game;
        this.playerBoard = playerBoard;

        seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard);
    }
}