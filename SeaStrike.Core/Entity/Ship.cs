namespace SeaStrike.Core.Entity;

public abstract class Ship
{
    public int width => occupiedTiles.Length;
    public readonly Tile[] occupiedTiles;
    public string name => GetType().Name;

    public Ship(int width) => occupiedTiles = new Tile[width];
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