namespace SeaStrike.Core.Entity.Game;

public class Player
{
    public Board board;

    internal Player(Board board) => this.board = board;

    public ShotResult Shoot(string tileStr)
    {
        Tile tile = board.targetGrid.GetTile(tileStr);

        if (tile.hasBeenHit)
            return null;

        tile.hasBeenHit = true;

        ShotResult result = new ShotResult
        {
            tile = tile,
            hit = tile.isOccupied,
            ship = tile.occupiedBy
        };

        board.opponentBoard.NotifyAllObservers();

        return result;
    }
}