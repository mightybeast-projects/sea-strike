namespace SeaStrike.Core.Entity;

public class Game
{
    public Player player1;
    public Player player2;
    public Player currentPlayer;
    public bool isOver => currentPlayer.board.opponentBoard.shipsAreSunk;

    public Game(Board player1Board, Board player2Board)
    {
        player1Board.Bind(player2Board);

        player1 = new Player(player1Board);
        player2 = new Player(player2Board);
        currentPlayer = player1;
    }

    public void HandleShot(string tileStr)
    {
        if (isOver)
            return;

        currentPlayer.Shoot(tileStr);

        if (!isOver)
            SwitchPlayer();
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == player1)
            currentPlayer = player2;
        else
            currentPlayer = player1;
    }
}