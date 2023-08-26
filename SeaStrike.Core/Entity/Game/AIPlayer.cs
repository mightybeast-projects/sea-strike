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

    internal void Shoot()
    {
        Tile[,] tiles = board.oceanGrid.tiles;

        int rndI = rnd.Next(tiles.GetLength(0));
        int rndJ = rnd.Next(tiles.GetLength(1));

        string randomTileStr = tiles[rndI, rndJ].notation;

        System.Console.WriteLine(randomTileStr);

        base.Shoot(randomTileStr);
    }
}