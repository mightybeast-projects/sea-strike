namespace SeaStrike.Core;

public class Board
{
    public readonly Grid oceanGrid;
    public readonly Grid targetGrid;
    public readonly List<Ship> ships;

    public Board()
    {
        oceanGrid = new Grid();
        targetGrid = new Grid();
        ships = new List<Ship>();
    }

    internal void AddHorizontalShip(Ship ship, string startTileStr)
    {
        Tile startTile = oceanGrid.GetTile(startTileStr);

        if (oceanGrid.tiles.GetLength(0) - (startTile.i + ship.width) < 0)
            return;

        ships.Add(ship);

        for (int i = 0; i < ship.width; i++)
        {
            Tile tile = oceanGrid.tiles[startTile.i + i, startTile.j];
            tile.occupiedBy = ship;
            ship.occupiedTiles[i] = tile;
        }
    }

    internal void AddVerticalShip(Ship ship, string startTileStr)
    {
        Tile startTile = oceanGrid.GetTile(startTileStr);

        if (oceanGrid.tiles.GetLength(1) - (startTile.j + ship.width) < 0)
            return;

        ships.Add(ship);

        for (int j = 0; j < ship.width; j++)
        {
            Tile tile = oceanGrid.tiles[startTile.i, startTile.j + j];
            tile.occupiedBy = ship;
            ship.occupiedTiles[j] = tile;
        }
    }
}