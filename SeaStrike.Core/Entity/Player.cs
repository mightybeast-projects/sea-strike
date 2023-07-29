namespace SeaStrike.Core.Entity;

public class Player
{
    public Board board;

    internal Player(Board board) => this.board = board;

    public void Shoot(string tileStr) =>
        board.targetGrid.GetTile(tileStr).hasBeenHit = true;
}