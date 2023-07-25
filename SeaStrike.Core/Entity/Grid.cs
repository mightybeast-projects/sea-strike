using SeaStrike.Core.Exceptions;

namespace SeaStrike.Core.Entity;

public class Grid
{
    internal readonly Tile[,] tiles;

    internal Grid()
    {
        tiles = new Tile[10, 10];

        for (int i = 0; i < tiles.GetLength(0); i++)
            for (int j = 0; j < tiles.GetLength(1); j++)
                tiles[i, j] = new Tile(i, j);
    }

    internal Tile GetTile(string tileNotation) =>
        tiles.Cast<Tile>().FirstOrDefault(tile => tile.notation == tileNotation)
        ?? throw new CannotFindSpecifiedTileException(tileNotation);
}