namespace SeaStrike.Core.Entity;

public enum Orientation { Horizontal, Vertical }

public abstract class Ship
{
    public readonly Tile[] occupiedTiles;

    public Orientation orientation =>
        occupiedTiles[0]?.j == occupiedTiles[1]?.j ?
            Orientation.Horizontal :
            Orientation.Vertical;
    public string name => GetType().Name;
    public int width => occupiedTiles.Length;
    public bool isSunk => occupiedTiles.All(tile =>
        tile is not null && tile.hasBeenHit);

    protected Ship(int width) => occupiedTiles = new Tile[width];

    private Ship(Tile[] occupiedTiles) => this.occupiedTiles = occupiedTiles;

    public override string ToString() =>
        GetType().Name + " (Width : " + width + ").";
}

public class Destroyer : Ship
{
    public Destroyer() : base(2) { }
}

public class Cruiser : Ship
{
    public Cruiser() : base(3) { }
}

public class Submarine : Ship
{
    public Submarine() : base(3) { }
}

public class Battleship : Ship
{
    public Battleship() : base(4) { }
}

public class Carrier : Ship
{
    public Carrier() : base(5) { }
}