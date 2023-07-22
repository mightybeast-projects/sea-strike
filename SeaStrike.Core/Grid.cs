namespace SeaStrike.Core;

public class Grid
{
    internal readonly Tile[,] tiles;

    public Grid()
    {
        tiles = new Tile[10, 10];

        for (int i = 0; i < tiles.GetLength(0); i++)
            for (int j = 0; j < tiles.GetLength(1); j++)
                tiles[i, j] = new Tile(i, j);
    }

    internal Tile GetTile(string notation) =>
        tiles.Cast<Tile>()
            .Where(tile => tile.notation == notation)
            .FirstOrDefault();
}