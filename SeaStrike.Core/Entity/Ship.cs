namespace SeaStrike.Core.Entity;

public abstract class Ship
{
    public readonly Tile[] occupiedTiles;

    internal bool isSunk => occupiedTiles.All(tile => tile.hasBeenHit)!;

    public string name => GetType().Name;
    public int width => occupiedTiles.Length;

    protected Ship(int width) => occupiedTiles = new Tile[width];
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