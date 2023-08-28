using System.Numerics;
namespace SeaStrike.Core.Entity.Game;

public class AIPlayer : Player
{
    private static Board InitializeRandomizedBoard =>
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build();

    private readonly Random rnd;
    private ShotResult anchorShot;
    private Vector2 shotVector = Vector2.Zero;
    private bool vectorizedHit;

    internal AIPlayer() : base(InitializeRandomizedBoard) =>
        rnd = new Random();

    internal AIPlayer(int seed) : base(InitializeRandomizedBoard) =>
        rnd = new Random(seed);

    internal ShotResult Shoot()
    {
        if (shotVector == Vector2.Zero)
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
            RotateShotVector();
        }

        return shotResult;
    }

    private ShotResult ShootVectorizedTile()
    {
        Tile nextTile = ChooseNextVectorizedTile();

        if (nextTile.hasBeenHit)
        {
            RotateShotVector();

            return Shoot();
        }

        ShotResult shotResult = base.Shoot(nextTile.notation);

        if (shotResult.hit)
        {
            vectorizedHit = true;
            IncreaseShotVectorLength();
        }
        else if (!shotResult.hit && vectorizedHit)
            InvertShotVector();
        else
            RotateShotVector();

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
        int nextTileI = Math.Clamp(
            anchorShot.tile.i + (int)shotVector.X,
            0,
            board.targetGrid.width - 1
        );
        int nextTileJ = Math.Clamp(
            anchorShot.tile.j + (int)shotVector.Y,
            0,
            board.targetGrid.height - 1
        );

        return board.targetGrid.tiles[nextTileI, nextTileJ];
    }

    private void IncreaseShotVectorLength()
    {
        if (shotVector.X > 0)
            shotVector.X++;
        if (shotVector.X < 0)
            shotVector.X--;
        if (shotVector.Y > 0)
            shotVector.Y++;
        if (shotVector.Y < 0)
            shotVector.Y--;
    }

    private void InvertShotVector()
    {
        if (ShotVectorIsNotNormalized())
            NormalizeShootingVector();

        shotVector *= -1;
    }

    private void RotateShotVector()
    {
        if (ShotVectorIsNotNormalized())
            NormalizeShootingVector();

        if (shotVector == Vector2.Zero)
            shotVector = Vector2.UnitY;
        else if (shotVector == Vector2.UnitY)
            shotVector = Vector2.UnitX;
        else if (shotVector == Vector2.UnitX)
            shotVector = -Vector2.UnitY;
        else if (shotVector == -Vector2.UnitY)
            shotVector = -Vector2.UnitX;
        else
            shotVector = Vector2.Zero;
    }

    private void NormalizeShootingVector()
    {
        int newX = 0;
        int newY = 0;

        if (shotVector.X > 0)
            newX = 1;
        if (shotVector.X < 0)
            newX = -1;
        if (shotVector.Y > 0)
            newY = 1;
        if (shotVector.Y < 0)
            newY = -1;

        shotVector = new Vector2(newX, newY);
    }

    private void ResetShootingBehaviour()
    {
        anchorShot = null;
        shotVector = Vector2.Zero;
        vectorizedHit = false;
    }

    private bool ShotVectorIsNotNormalized() =>
        shotVector.X > 1 ||
        shotVector.X < 1 ||
        shotVector.Y > 1 ||
        shotVector.Y < 1;
}