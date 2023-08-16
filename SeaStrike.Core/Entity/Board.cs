using SeaStrike.Core.Exceptions;

namespace SeaStrike.Core.Entity;

public class Board
{
    public readonly List<IBoardObserver> observers;
    public readonly Grid oceanGrid;
    public readonly List<Ship> ships;
    public List<Ship> shipsPool { get; private set; }
    public Board opponentBoard { get; private set; }
    public Grid targetGrid => opponentBoard?.oceanGrid;

    internal bool shipsAreSunk => ships.All(ship => ship.isSunk);

    internal Board()
    {
        observers = new List<IBoardObserver>();
        oceanGrid = new Grid();
        ships = new List<Ship>();
        shipsPool = DefaultShipsPool;
    }

    internal void RandomizeShips()
    {
        ResetBoard();

        Ship[] shipsPoolArr = shipsPool.ToArray();

        foreach (Ship ship in shipsPoolArr)
            while (!ships.Contains(ship))
                TryToAddShipRandomly(ship);
    }

    internal void AddHorizontalShip(Ship ship, string startTileStr)
    {
        ValidateShipType(ship);

        Tile startTile = oceanGrid.GetTile(startTileStr);
        int minWidth = oceanGrid.tiles.GetLength(0) - (startTile.i + ship.width);

        ValidateShipWidth(minWidth, startTile);

        Tile[] tilesToOccupy = new Tile[ship.width];
        for (int i = 0; i < ship.width; i++)
            tilesToOccupy[i] = oceanGrid.tiles[startTile.i + i, startTile.j];

        ValidateTilesToOccupy(tilesToOccupy);

        AddShip(ship, tilesToOccupy);
    }

    internal void AddVerticalShip(Ship ship, string startTileStr)
    {
        ValidateShipType(ship);

        Tile startTile = oceanGrid.GetTile(startTileStr);
        int minWidth = oceanGrid.tiles.GetLength(1) - (startTile.j + ship.width);

        ValidateShipWidth(minWidth, startTile);

        Tile[] tilesToOccupy = new Tile[ship.width];
        for (int j = 0; j < ship.width; j++)
            tilesToOccupy[j] = oceanGrid.tiles[startTile.i, startTile.j + j];

        ValidateTilesToOccupy(tilesToOccupy);

        AddShip(ship, tilesToOccupy);
    }

    internal void ClearOceanGrid()
    {
        ResetBoard();

        NotifyAllObservers();
    }

    internal void RemoveShipAt(string occupiedTileStr)
    {
        Tile occupiedTile = oceanGrid.GetTile(occupiedTileStr);
        Ship ship = occupiedTile.occupiedBy;

        if (ship is null)
            return;

        foreach (Tile tile in ship.occupiedTiles)
            tile.occupiedBy = null;

        ships.Remove(ship);
        shipsPool.Add(ship);

        NotifyAllObservers();
    }

    internal void Bind(Board opponentBoard)
    {
        this.opponentBoard = opponentBoard;
        opponentBoard.opponentBoard = this;
    }

    internal void Subscribe(IBoardObserver observer) => observers.Add(observer);

    internal void NotifyAllObservers() => observers.ForEach(o => o.Notify());

    private void ResetBoard()
    {
        oceanGrid.Reset();
        ships.Clear();
        shipsPool = DefaultShipsPool;
    }

    private void TryToAddShipRandomly(Ship ship)
    {
        int tileI = new Random().Next(0, oceanGrid.width);
        int tileJ = new Random().Next(0, oceanGrid.height);
        Tile startTile = oceanGrid.tiles[tileI, tileJ];

        try
        {
            if (new Random().Next(2) == 0)
                AddHorizontalShip(ship, startTile.notation);
            else
                AddVerticalShip(ship, startTile.notation);
        }
        catch (SeaStrikeCoreException) { return; }
    }

    private void AddShip(Ship ship, Tile[] tilesToOccupy)
    {
        for (int i = 0; i < tilesToOccupy.Length; i++)
        {
            Tile tile = tilesToOccupy[i];
            tile.occupiedBy = ship;
            ship.occupiedTiles[i] = tile;
        }

        ships.Add(ship);
        shipsPool.Remove(shipsPool.Find(s => s.GetType() == ship.GetType()));

        NotifyAllObservers();
    }

    private void ValidateShipType(Ship ship)
    {
        if (!shipsPool.Contains(shipsPool.Find(s => s.GetType() == ship.GetType())))
            throw new CannotAddAlreadyPlacedShipException(ship);
    }

    private void ValidateShipWidth(int minWidth, Tile startTile)
    {
        if (minWidth < 0)
            throw new CannotFitShipException(startTile);
    }

    private void ValidateTilesToOccupy(Tile[] tilesToOccupy)
    {
        foreach (Tile tile in tilesToOccupy)
            if (tile.isOccupied)
                throw new TileIsOccupiedByOtherShipException(tile);
    }

    private List<Ship> DefaultShipsPool => new List<Ship>()
    {
        new Destroyer(),
        new Cruiser(),
        new Submarine(),
        new Battleship(),
        new Carrier()
    };
}