namespace SeaStrike.Core;

internal class Tile
{
    internal readonly int i;
    internal readonly int j;
    internal string notation => (char)(j + 65) + (i + 1).ToString();

    public Tile(int i, int j)
    {
        this.i = i;
        this.j = j;
    }
}