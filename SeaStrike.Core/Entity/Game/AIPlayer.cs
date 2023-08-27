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
                SwitchShootingVector();
                System.Console.WriteLine(anchor + " " + shootingVector.ToString());
            }
        }
        else
        {
            System.Console.WriteLine(anchor + " " + shootingVector.ToString());
            int nextTileI = Math.Clamp(anchor.tile.i + (int)shootingVector.X, 0, board.targetGrid.width - 1);
            int nextTileJ = Math.Clamp(anchor.tile.j + (int)shootingVector.Y, 0, board.targetGrid.height - 1);

            Tile nextTile = board.targetGrid.tiles[nextTileI, nextTileJ];

            System.Console.WriteLine("NextTile:" + nextTile);

            if (nextTile.hasBeenHit)
            {
                SwitchShootingVector();
                return Shoot();
            }

            result = base.Shoot(nextTile.notation);

            if (result.hit)
                IncreaseShootingVectorLength();

            if (result.sunk ?? false)
            {
                anchor = null;
                shootingVector = Vector2.Zero;
            }
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

    private void SwitchShootingVector()
    {
        if (shootingVector == Vector2.Zero)
            shootingVector = new Vector2(0, 1);
        else if (shootingVector == new Vector2(0, 1))
            shootingVector = new Vector2(1, 0);
        else if (shootingVector == new Vector2(1, 0))
            shootingVector = new Vector2(0, -1);
        else if (shootingVector == new Vector2(0, -1))
            shootingVector = new Vector2(-1, 0);
        else
            shootingVector = Vector2.Zero;
    }
}