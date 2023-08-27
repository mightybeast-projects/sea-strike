using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.Core.Tests.EntityTests.SeaStrikeGameTests;

[TestFixture]
public class AIPlayerTests
{
    [TestCaseSource(nameof(cases))]
    public void SeededShoot_ShouldChooseExpectedTile(
        int seed,
        string expectedTileStr)
    {
        Board playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        Player player = new Player(playerBoard);
        AIPlayer ai = new AIPlayer(seed);

        player.board.Bind(ai.board);

        ai.Shoot().tile.notation.Should().Be(expectedTileStr);
    }

    [Test]
    public void SeededShotSequence_ShouldChooseExpectedTiles()
    {
        Board playerBoard = new BoardBuilder()
            .AddVerticalShip(new Destroyer())
                .AtPosition("B3")
            .Build();

        Player player = new Player(playerBoard);
        AIPlayer ai = new AIPlayer(001);

        player.board.Bind(ai.board);

        ShootResult firstShot = ai.Shoot();
        firstShot.tile.notation.Should().Be("B3");
        firstShot.hit.Should().BeTrue();
        firstShot.ship.Should().BeAssignableTo<Destroyer>();

        ShootResult secondShot = ai.Shoot();
        secondShot.tile.notation.Should().Be("C3");
        secondShot.hit.Should().BeTrue();
        secondShot.ship.Should().BeAssignableTo<Destroyer>();
        secondShot.sunk.Should().BeTrue();
    }

    [Test]
    public void AIPlayer_ShouldNotShoot_AtAlreadyShotTile()
    {
        Board playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        Player player = new Player(playerBoard);
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