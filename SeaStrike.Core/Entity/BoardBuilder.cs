namespace SeaStrike.Core.Entity;

public class BoardBuilder
{
    internal Board board;

    private Ship shipToAdd;
    private Action<Ship, string> additionMethod;

    public BoardBuilder() => board = new Board();

    public Board Build() => board;

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

    public BoardBuilder BindOpponentBoard(Board opponenetBoard)
    {
        board.Bind(opponenetBoard);

        return this;
    }
}