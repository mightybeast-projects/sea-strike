using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.Core.Tests.EntityTests.SeaStrikeGameTests;

[TestFixture]
public class AIPlayerTests
{
    private ShootResult result;

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

    [Test]
    public void AIPlayer_ShouldChooseShootingVector()
    {
        Board playerBoard = new BoardBuilder()
            .AddVerticalShip(new Destroyer())
                .AtPosition("B3")
            .Build();

        Player player = new Player(playerBoard);
        AIPlayer ai = new AIPlayer(001);

        player.board.Bind(ai.board);

        result = ai.Shoot();
        result.tile.notation.Should().Be("B3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Destroyer>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("C3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Destroyer>();
        result.sunk.Should().BeTrue();
    }

    [Test]
    public void AIPlayer_ShouldIncreaseShootingVectorLength()
    {
        Board playerBoard = new BoardBuilder()
            .AddVerticalShip(new Submarine())
                .AtPosition("B3")
            .Build();

        Player player = new Player(playerBoard);
        AIPlayer ai = new AIPlayer(001);

        player.board.Bind(ai.board);

        result = ai.Shoot();
        result.tile.notation.Should().Be("B3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Submarine>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("C3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Submarine>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("D3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Submarine>();
        result.sunk.Should().BeTrue();
    }

    [Test]
    public void AIPlayer_ShouldRotateShootingVector_OnMiss()
    {
        Board playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("B3")
            .Build();

        Player player = new Player(playerBoard);
        AIPlayer ai = new AIPlayer(001);

        player.board.Bind(ai.board);

        result = ai.Shoot();
        result.tile.notation.Should().Be("B3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Destroyer>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("C3");
        result.hit.Should().BeFalse();

        result = ai.Shoot();
        result.tile.notation.Should().Be("B4");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Destroyer>();
        result.sunk.Should().BeTrue();
    }

    [Test]
    public void AIPlayer_ShouldInvertShootingVector_OnMiss()
    {
        Board playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Battleship())
                .AtPosition("B1")
            .Build();

        Player player = new Player(playerBoard);
        AIPlayer ai = new AIPlayer(001);

        player.board.Bind(ai.board);

        result = ai.Shoot();
        result.tile.notation.Should().Be("B3");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Battleship>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("C3");
        result.hit.Should().BeFalse();

        result = ai.Shoot();
        result.tile.notation.Should().Be("B4");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Battleship>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("B5");
        result.hit.Should().BeFalse();

        result = ai.Shoot();
        result.tile.notation.Should().Be("B2");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Battleship>();

        result = ai.Shoot();
        result.tile.notation.Should().Be("B1");
        result.hit.Should().BeTrue();
        result.ship.Should().BeAssignableTo<Battleship>();
        result.sunk.Should().BeTrue();
    }

    private static TestCaseData[] cases =
    {
        new TestCaseData(001, "B3"),
        new TestCaseData(002, "E8"),
        new TestCaseData(003, "G3")
    };
}