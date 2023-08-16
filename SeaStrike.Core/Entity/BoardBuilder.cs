namespace SeaStrike.Core.Entity;

public class BoardBuilder
{
    public Ship[] shipsPool => board.shipsPool.ToArray();

    private Board board;
    private Ship shipToAdd;
    private Action<Ship, string> additionMethod;

    public BoardBuilder() => board = new Board();
    public BoardBuilder(Board board) => this.board = board;

    public Board Build() => board;

    public BoardBuilder RandomizeShipsStartingPosition()
    {
        board.RandomizeShips();

        return this;
    }

    public BoardBuilder AddHorizontalShip(Ship ship)
    {
        shipToAdd = ship;
        additionMethod = board.AddHorizontalShip;

        return this;
    }

    public BoardBuilder AddVerticalShip(Ship ship)
    {
        shipToAdd = ship;
        additionMethod = board.AddVerticalShip;

        return this;
    }

    public BoardBuilder AtPosition(string tileStr)
    {
        additionMethod(shipToAdd, tileStr);
        shipToAdd = null;
        additionMethod = null;

        return this;
    }

    public BoardBuilder RemoveShipAt(string tileStr)
    {
        board.RemoveShipAt(tileStr);

        return this;
    }

    public BoardBuilder ClearOceanGrid()
    {
        board.ClearOceanGrid();

        return this;
    }

    public BoardBuilder Subscribe(IBoardObserver observer)
    {
        board.Subscribe(observer);

        return this;
    }
}