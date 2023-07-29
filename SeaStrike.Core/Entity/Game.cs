namespace SeaStrike.Core.Entity;

public class Game
{
    public Player player1;
    public Player player2;
    public Player currentPlayer;

    public Game(Board player1Board, Board player2Board)
    {
        player1 = new Player(player1Board);
        player2 = new Player(player2Board);
        currentPlayer = player1;
    }
}