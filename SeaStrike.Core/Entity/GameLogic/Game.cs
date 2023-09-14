using SeaStrike.Core.Entity.GameLogic.Utility;

namespace SeaStrike.Core.Entity.GameLogic;

public class Game
{
    public Player currentPlayer { get; private set; }
    internal readonly Player player;
    internal readonly Player opponent;

    public bool isOver => currentPlayer.board.opponentBoard.shipsAreSunk;

    public Game(Board playerBoard)
    {
        player = new Player(playerBoard);
        opponent = new AIPlayer();

        StartGame(player);
    }

    public Game(
        Board playerBoard,
        Board opponentBoard,
        bool opponentMovesFirst = false)
    {
        player = new Player(playerBoard);
        opponent = new Player(opponentBoard);

        if (opponentMovesFirst)
            StartGame(opponent);
        else
            StartGame(player);
    }

    public ShotResult HandleCurrentPlayerShot(string tileStr)
    {
        if (isOver)
            return null;

        ShotResult result = currentPlayer.Shoot(tileStr);

        if (!isOver)
            SwitchPlayer();

        return result;
    }

    public ShotResult HandleAIPlayerShot()
    {
        if (isOver)
            return null;

        ShotResult result = (currentPlayer as AIPlayer)?.Shoot();

        if (!isOver && result is not null)
            SwitchPlayer();

        return result;
    }

    private void StartGame(Player firstMovePlayer)
    {
        player.board.Bind(opponent.board);

        currentPlayer = firstMovePlayer;
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == player)
            currentPlayer = opponent;
        else
            currentPlayer = player;
    }
}