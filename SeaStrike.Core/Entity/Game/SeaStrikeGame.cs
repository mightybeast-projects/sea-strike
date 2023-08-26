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

        playerBoard.Bind(opponent.board);

        currentPlayer = player;
    }

    public SeaStrikeGame(Board playerBoard, Board opponentBoard)
    {
        player = new Player(playerBoard);
        opponent = new Player(opponentBoard);

        playerBoard.Bind(opponentBoard);

        currentPlayer = player;
    }

    public ShootResult HandleCurrentPlayerShot(string tileStr)
    {
        if (isOver)
            return null;

        ShootResult result = currentPlayer.Shoot(tileStr);

        if (!isOver)
            SwitchPlayer();

        return result;
    }

    public ShootResult HandleAIPlayerShot()
    {
        if (isOver)
            return null;

        ShootResult result = (currentPlayer as AIPlayer)?.Shoot();

        if (!isOver && result is not null)
            SwitchPlayer();

        return result;
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == player)
            currentPlayer = opponent;
        else
            currentPlayer = player;
    }
}