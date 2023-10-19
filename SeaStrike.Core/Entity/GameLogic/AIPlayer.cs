using SeaStrike.Core.Entity.GameLogic.Utility;

namespace SeaStrike.Core.Entity.GameLogic;

public class AIPlayer : Player
{
    private static Board InitializeRandomizedBoard =>
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build();

    private readonly Random rnd;
    private List<ShotResult> anchorShots = new List<ShotResult>();
    private ShotVector shotVector = new ShotVector();
    private bool vectorizedHit;

    internal AIPlayer() : base(InitializeRandomizedBoard) =>
        rnd = new Random();

    internal AIPlayer(int seed) : base(InitializeRandomizedBoard) =>
        rnd = new Random(seed);

    internal ShotResult Shoot()
    {
        if (anchorShots.Count == 0)
            return ShootRandomTile();
        else
            return ShootAnchoredTile();
    }

    private ShotResult ShootRandomTile()
    {
        Tile randomTile = ChooseRandomTile();
        ShotResult shotResult = base.Shoot(randomTile.notation);

        if (shotResult.hit)
        {
            anchorShots.Add(shotResult);
            shotVector.Rotate();
        }

        return shotResult;
    }

    private ShotResult ShootAnchoredTile()
    {
        Tile nextTile = ChooseNextVectorizedTile();

        if (nextTile.hasBeenHit)
        {
            shotVector.Rotate();

            return Shoot();
        }

        ShotResult shotResult = base.Shoot(nextTile.notation);

        if (shotResult.hit)
        {
            if (shotResult.ship == anchorShots[0].ship)
            {
                vectorizedHit = true;
                shotVector.Extend();
            }
            else
            {
                anchorShots.Add(shotResult);
                shotVector.Rotate();
            }
        }
        else if (!shotResult.hit && vectorizedHit)
            shotVector.Invert();
        else
            shotVector.Rotate();

        if (shotResult.sunk ?? false)
            ResetShootingBehaviour();

        return shotResult;
    }

    private Tile ChooseRandomTile()
    {
        Tile[,] opponentTiles = board.targetGrid.tiles;

        int rndI = rnd.Next(opponentTiles.GetLength(0));
        int rndJ = rnd.Next(opponentTiles.GetLength(1));

        Tile randomTile = opponentTiles[rndI, rndJ];

        if (randomTile.hasBeenHit)
            return ChooseRandomTile();

        return randomTile;
    }

    private Tile ChooseNextVectorizedTile()
    {
        int widthMax = board.targetGrid.width - 1;
        int heightMax = board.targetGrid.height - 1;

        int nextTileI = Math.Clamp(
            anchorShots[0].tile.i + shotVector.x * shotVector.length,
            0,
            widthMax
        );
        int nextTileJ = Math.Clamp(
            anchorShots[0].tile.j + shotVector.y * shotVector.length,
            0,
            heightMax
        );

        return board.targetGrid.tiles[nextTileI, nextTileJ];
    }

    private void ResetShootingBehaviour()
    {
        anchorShots.RemoveAt(0);
        shotVector = new ShotVector();
        vectorizedHit = false;
    }
}