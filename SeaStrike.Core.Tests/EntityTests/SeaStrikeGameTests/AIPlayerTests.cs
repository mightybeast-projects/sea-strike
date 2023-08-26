using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.Core.Tests.EntityTests.SeaStrikeGameTests;

[TestFixture]
public class AIPlayerTests
{
    private Player player;

    [SetUp]
    public void SetUp()
    {
        Board playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("D4")
            .Build();

        player = new Player(playerBoard);
    }

    [TestCaseSource(nameof(cases))]
    public void SeededShoot_ShouldChooseExpectedTile(
        int seed,
        string expectedTileStr)
    {
        AIPlayer ai = new AIPlayer(seed);

        player.board.Bind(ai.board);

        ai.Shoot();

        player.board.oceanGrid.GetTile(expectedTileStr).hasBeenHit
            .Should().BeTrue();
    }

    [Test]
    public void AIPlayer_ShouldNotShoot_AtAlreadyShotTile()
    {
        AIPlayer ai = new AIPlayer();

        player.board.Bind(ai.board);

        for (int i = 0; i < 100; i++)
            ai.Shoot();

        Tile[,] playerTiles = player.board.oceanGrid.tiles;

        for (int i = 0; i < playerTiles.GetLength(0); i++)
            for (int j = 0; j < playerTiles.GetLength(1); j++)
                playerTiles[i, j].hasBeenHit.Should().BeTrue();
    }

    private static TestCaseData[] cases =
    {
        new TestCaseData(001, "B3"),
        new TestCaseData(002, "E8"),
        new TestCaseData(003, "G3")
    };
}