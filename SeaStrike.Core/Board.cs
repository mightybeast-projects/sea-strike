namespace SeaStrike.Core;

public class Board
{
    public readonly Grid oceanGrid;
    public readonly Grid targetGrid;

    public Board()
    {
        oceanGrid = new Grid();
        targetGrid = new Grid();
    }
}