namespace SeaStrike.Core;

public class Ship
{
    public int width => occupiedTiles.Length;
    public readonly Tile[] occupiedTiles;

    public Ship(int width) => occupiedTiles = new Tile[width];
}