using System.Text;

namespace SeaStrike.Core.Entity.Game;

public class ShotResult
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