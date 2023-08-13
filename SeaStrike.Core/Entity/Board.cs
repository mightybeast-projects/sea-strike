using SeaStrike.Core.Exceptions;

namespace SeaStrike.Core.Entity;

public class Board
{
    public Grid oceanGrid { get; private set; }
    public List<Ship> ships { get; private set; }
    public Board opponentBoard { get; private set; }
    public Grid targetGrid => opponentBoard?.oceanGrid;
    public List<Ship> shipsPool;
    public List<IBoardObserver> observers;

    internal bool shipsAreSunk => ships.All(ship => ship.isSunk);

    internal Board()
    {
        observers = new List<IBoardObserver>();

        ResetShipsPool();
        ResetBoard();
    }

    internal void RandomizeShips()
    {
        ClearOceanGrid();

        Ship[] remainingShips = shipsPool.ToArray();

        foreach (Ship ship in remainingShips)
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
        oceanGrid = new Grid();
        ships = new List<Ship>();
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

    private void ResetShipsPool() => shipsPool = new List<Ship>()
    {
        new Destroyer(),
        new Cruiser(),
        new Submarine(),
        new Battleship(),
        new Carrier()
    };
}