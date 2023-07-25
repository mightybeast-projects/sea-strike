namespace SeaStrike.Core.Entity;

public class Tile
{
    internal readonly int i;
    internal readonly int j;
    internal bool isOccupied => occupiedBy is not null;
    internal Ship occupiedBy;

    internal string notation => (char)(j + 65) + (i + 1).ToString();

    internal Tile(int i, int j)
    {
        this.i = i;
        this.j = j;
    }
}