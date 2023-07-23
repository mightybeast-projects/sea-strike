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

    internal void AddHorizontalShip(Ship ship, Tile startTile)
    {
        ships.Add(ship);

        for (int i = 0; i < ship.width; i++)
        {
            Tile tile = oceanGrid.tiles[startTile.i + i, startTile.j];
            tile.occupiedBy = ship;
            ship.occupiedTiles[i] = tile;
        }
    }
}