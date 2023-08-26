namespace SeaStrike.Core.Entity.Game;

public class AIPlayer : Player
{
    private static Board InitializeRandomizedBoard =>
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build();

    private readonly Random rnd;

    internal AIPlayer() : base(InitializeRandomizedBoard) =>
        rnd = new Random();

    internal AIPlayer(int seed) : base(InitializeRandomizedBoard) =>
        rnd = new Random(seed);

    internal ShootResult Shoot() => base.Shoot(ChooseRandomTile().notation);

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
}