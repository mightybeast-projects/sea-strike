using System.Numerics;
namespace SeaStrike.Core.Entity.Game;

public class AIPlayer : Player
{
    private static Board InitializeRandomizedBoard =>
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build();

    private readonly Random rnd;
    private ShootResult anchor;
    private Vector2 shootingVector = Vector2.Zero;
    private bool vectorizedHit;

    internal AIPlayer() : base(InitializeRandomizedBoard) =>
        rnd = new Random();

    internal AIPlayer(int seed) : base(InitializeRandomizedBoard) =>
        rnd = new Random(seed);

    internal ShootResult Shoot()
    {
        ShootResult result = null;

        if (shootingVector == Vector2.Zero)
        {
            Tile randomTile = ChooseRandomTile();
            result = base.Shoot(randomTile.notation);

            System.Console.WriteLine(randomTile);

            if (result.hit)
            {
                anchor = result;
                RotateShootingVector();
                System.Console.WriteLine(anchor + " " + shootingVector.ToString());
            }
        }
        else
        {
            System.Console.WriteLine(anchor + " " + shootingVector.ToString());

            int nextTileI = Math.Clamp(anchor.tile.i + (int)shootingVector.X,
                0,
                board.targetGrid.width - 1);
            int nextTileJ = Math.Clamp(anchor.tile.j + (int)shootingVector.Y,
                0,
                board.targetGrid.height - 1);

            Tile nextTile = board.targetGrid.tiles[nextTileI, nextTileJ];

            System.Console.WriteLine("NextTile: " + nextTile);

            if (nextTile.hasBeenHit)
            {
                RotateShootingVector();
                return Shoot();
            }

            result = base.Shoot(nextTile.notation);

            if (result.hit)
            {
                vectorizedHit = true;
                IncreaseShootingVectorLength();
            }
            else if (!result.hit && vectorizedHit)
                InvertShootingVector();
            else
                RotateShootingVector();

            if (result.sunk ?? false)
                ResetShootingBehaviour();
        }

        return result;
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

    private void IncreaseShootingVectorLength()
    {
        if (shootingVector.X > 0)
            shootingVector.X++;
        if (shootingVector.X < 0)
            shootingVector.X--;
        if (shootingVector.Y > 0)
            shootingVector.Y++;
        if (shootingVector.Y < 0)
            shootingVector.Y--;
    }

    private void InvertShootingVector()
    {
        if (ShootingVectorIsLong())
            ResetShootingVector();

        shootingVector *= -1;
    }

    private void RotateShootingVector()
    {
        if (ShootingVectorIsLong())
            ResetShootingVector();

        if (shootingVector == Vector2.Zero)
            shootingVector = Vector2.UnitY;
        else if (shootingVector == Vector2.UnitY)
            shootingVector = Vector2.UnitX;
        else if (shootingVector == Vector2.UnitX)
            shootingVector = -Vector2.UnitY;
        else if (shootingVector == -Vector2.UnitY)
            shootingVector = -Vector2.UnitX;
        else
            shootingVector = Vector2.Zero;
    }

    private void ResetShootingVector()
    {
        int newX = 0;
        int newY = 0;

        if (shootingVector.X > 0)
            newX = 1;
        if (shootingVector.X < 0)
            newX = -1;
        if (shootingVector.Y > 0)
            newY = 1;
        if (shootingVector.Y < 0)
            newY = -1;

        shootingVector = new Vector2(newX, newY);
    }

    private void ResetShootingBehaviour()
    {
        anchor = null;
        shootingVector = Vector2.Zero;
        vectorizedHit = false;
    }

    private bool ShootingVectorIsLong() =>
        shootingVector.X > 1 ||
        shootingVector.X < 1 ||
        shootingVector.Y > 1 ||
        shootingVector.Y < 1;
}