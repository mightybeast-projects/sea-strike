namespace SeaStrike.Core.Entity;

public class BoardBuilder
{
    private Board board;
    private Ship shipToAdd;
    private Action<Ship, string> additionMethod;

    public BoardBuilder() => board = new Board();

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
}