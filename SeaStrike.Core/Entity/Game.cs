namespace SeaStrike.Core.Entity;

public class Game
{
    public Player player;
    public Player opponent;
    public Player currentPlayer;
    public bool isOver => currentPlayer.board.opponentBoard.shipsAreSunk;

    public Game(Board playerBoard)
    {
        player = new Player(playerBoard);
        opponent = new AIPlayer();

        playerBoard.Bind(opponent.board);

        currentPlayer = player;
    }

    public Game(Board playerBoard, Board opponentBoard)
    {
        player = new Player(playerBoard);
        opponent = new Player(opponentBoard);

        playerBoard.Bind(opponentBoard);

        currentPlayer = player;
    }

    public ShootResult HandleShot(string tileStr)
    {
        if (isOver)
            return null;

        ShootResult result = currentPlayer.Shoot(tileStr);

        if (!isOver)
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