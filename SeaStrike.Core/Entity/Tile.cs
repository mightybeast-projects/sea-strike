namespace SeaStrike.Core.Entity;

public class Tile
{
    public readonly int i;
    public readonly int j;
    public Ship occupiedBy { get; internal set; }
    public bool hasBeenHit { get; internal set; }
    public bool isOccupied => occupiedBy is not null;
    public string notation => (char)(j + 65) + (i + 1).ToString();

    public override string ToString() => notation;

    internal Tile(int i, int j)
    {
        this.i = i;
        this.j = j;
    }

    private Tile(int i, int j, Ship occupiedBy)
    {
        this.i = i;
        this.j = j;
        this.occupiedBy = occupiedBy;
    }
}