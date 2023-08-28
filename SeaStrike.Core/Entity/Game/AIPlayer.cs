using System.Numerics;
using SeaStrike.Core.Entity.Game.Utility;

namespace SeaStrike.Core.Entity.Game;

public class AIPlayer : Player
{
    private static Board InitializeRandomizedBoard =>
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build();

    private readonly Random rnd;
    private ShotResult anchorShot;
    private ShotVector shotVector = new ShotVector();
    private bool vectorizedHit;

    internal AIPlayer() : base(InitializeRandomizedBoard) =>
        rnd = new Random();

    internal AIPlayer(int seed) : base(InitializeRandomizedBoard) =>
        rnd = new Random(seed);

    internal ShotResult Shoot()
    {
        if (shotVector.isZero)
            return ShootRandomTile();
        else
            return ShootVectorizedTile();
    }

    private ShotResult ShootRandomTile()
    {
        Tile randomTile = ChooseRandomTile();
        ShotResult shotResult = base.Shoot(randomTile.notation);

        if (shotResult.hit)
        {
            anchorShot = shotResult;
            shotVector.Rotate();
        }

        return shotResult;
    }

    private ShotResult ShootVectorizedTile()
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
            vectorizedHit = true;
            shotVector.Extend();
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
            anchorShot.tile.i + shotVector.x * shotVector.length, 0, widthMax
        );
        int nextTileJ = Math.Clamp(
            anchorShot.tile.j + shotVector.y * shotVector.length, 0, heightMax
        );

        return board.targetGrid.tiles[nextTileI, nextTileJ];
    }

    private void ResetShootingBehaviour()
    {
        anchorShot = null;
        shotVector = new ShotVector();
        vectorizedHit = false;
    }
}