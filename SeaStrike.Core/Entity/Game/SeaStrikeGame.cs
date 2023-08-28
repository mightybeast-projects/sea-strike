namespace SeaStrike.Core.Entity.Game;

public class SeaStrikeGame
{
    public Player currentPlayer { get; private set; }
    internal readonly Player player;
    internal readonly Player opponent;

    public bool isOver => currentPlayer.board.opponentBoard.shipsAreSunk;

    public SeaStrikeGame(Board playerBoard)
    {
        player = new Player(playerBoard);
        opponent = new AIPlayer();

        StartGame();
    }

    public SeaStrikeGame(Board playerBoard, Board opponentBoard)
    {
        player = new Player(playerBoard);
        opponent = new Player(opponentBoard);

        StartGame();
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

    private void StartGame()
    {
        player.board.Bind(opponent.board);

        currentPlayer = player;
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == player)
            currentPlayer = opponent;
        else
            currentPlayer = player;
    }
}