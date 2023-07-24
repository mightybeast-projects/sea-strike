namespace SeaStrike.Core;

public class BoardBuilder
{
    public Board board;

    public BoardBuilder Init()
    {
        board = new Board();

        return this;
    }

    public BoardBuilder AddHorizontalShip(Ship ship, string tileStr)
    {
        board.AddHorizontalShip(ship, tileStr);

        return this;
    }

    public BoardBuilder AddVerticalShip(Ship ship, string tileStr)
    {
        board.AddVerticalShip(ship, tileStr);

        return this;
    }
}