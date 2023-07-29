namespace SeaStrike.Core.Entity;

public class Player
{
    public Board board;

    internal Player(Board board) => this.board = board;

    public ShootResult Shoot(string tileStr)
    {
        Tile tile = board.targetGrid.GetTile(tileStr);

        if (tile.hasBeenHit)
            return null;

        tile.hasBeenHit = true;

        ShootResult result = new ShootResult
        {
            hit = tile.isOccupied,
            ship = tile.occupiedBy
        };

        return result;
    }
}

public class ShootResult
{
    public bool hit;
    public Ship ship;
    public bool? sunk => ship?.isSunk;
}