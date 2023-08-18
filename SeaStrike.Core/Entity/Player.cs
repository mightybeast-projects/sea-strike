using System.Text;
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
            tile = tile,
            hit = tile.isOccupied,
            ship = tile.occupiedBy
        };

        board.opponentBoard.NotifyAllObservers();

        return result;
    }
}

public class ShootResult
{
    public Tile tile;
    public bool hit;
    public Ship ship;
    public bool? sunk => ship?.isSunk;

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(tile + " : ");

        if (hit)
        {
            builder.Append("Hit. ");
            builder.Append(ship);
            if ((bool)sunk)
                builder.Append(" Sunk.");
        }
        else
            builder.Append("Miss.");

        return builder.ToString();
    }
}