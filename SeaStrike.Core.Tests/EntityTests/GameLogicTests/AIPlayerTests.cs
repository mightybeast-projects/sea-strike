using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.Core.Entity.GameLogic.Utility;

namespace SeaStrike.Core.Tests.EntityTests.GameLogicTests;

[TestFixture]
public class AIPlayerTests
{
    private Board playerBoard;
    private Player player;
    private AIPlayer ai;
    private ShotResult shotResult;

    [Test]
    public void AIPlayer_ShouldNotShoot_AtAlreadyShotTile()
    {
        playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        player = new Player(playerBoard);
        ai = new AIPlayer();

        player.board.Bind(ai.board);

        for (int i = 0; i < 100; i++)
            ai.Shoot();

        Tile[,] playerTiles = player.board.oceanGrid.tiles;

        for (int i = 0; i < playerTiles.GetLength(0); i++)
            for (int j = 0; j < playerTiles.GetLength(1); j++)
                playerTiles[i, j].hasBeenHit.Should().BeTrue();
    }

    [TestCaseSource(nameof(cases))]
    public void SeededShoot_ShouldChooseExpectedTile(
        int seed,
        string expectedTileStr)
    {
        playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        BindSeededAIPlayer(seed);

        ai.Shoot().tile.notation.Should().Be(expectedTileStr);
    }

    [Test]
    public void AIPlayer_ShouldChooseShotVector()
    {
        playerBoard = new BoardBuilder()
            .AddVerticalShip(new Destroyer())
                .AtPosition("B3")
            .Build();

        BindSeededAIPlayer(001);

        shotResult = ai.Shoot();
        AssertHitShot<Destroyer>("B3");

        shotResult = ai.Shoot();
        AssertSunkShot<Destroyer>("C3");
    }

    [Test]
    public void AIPlayer_ShouldExtendShotVector()
    {
        playerBoard = new BoardBuilder()
            .AddVerticalShip(new Submarine())
                .AtPosition("B3")
            .Build();

        BindSeededAIPlayer(001);

        shotResult = ai.Shoot();
        AssertHitShot<Submarine>("B3");

        shotResult = ai.Shoot();
        AssertHitShot<Submarine>("C3");

        shotResult = ai.Shoot();
        AssertSunkShot<Submarine>("D3");
    }

    [Test]
    public void AIPlayer_ShouldRotateShotVector_OnMiss()
    {
        playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("B3")
            .Build();

        BindSeededAIPlayer(001);

        shotResult = ai.Shoot();
        AssertHitShot<Destroyer>("B3");

        shotResult = ai.Shoot();
        AssertMissShot("C3");

        shotResult = ai.Shoot();
        AssertSunkShot<Destroyer>("B4");
    }

    [Test]
    public void AIPlayer_ShouldInvertShotVector_OnMiss()
    {
        playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Battleship())
                .AtPosition("B1")
            .Build();

        BindSeededAIPlayer(001);

        shotResult = ai.Shoot();
        AssertHitShot<Battleship>("B3");

        shotResult = ai.Shoot();
        AssertMissShot("C3");

        shotResult = ai.Shoot();
        AssertHitShot<Battleship>("B4");

        shotResult = ai.Shoot();
        AssertMissShot("B5");

        shotResult = ai.Shoot();
        AssertHitShot<Battleship>("B2");

        shotResult = ai.Shoot();
        AssertSunkShot<Battleship>("B1");
    }

    [Test]
    public void PlayerAI_ShouldAddNewAnchor_OnShot_WithDifferentShip()
    {
        playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("B3")
            .AddHorizontalShip(new Submarine())
                .AtPosition("C3")
            .Build();

        BindSeededAIPlayer(001);

        shotResult = ai.Shoot();
        AssertHitShot<Destroyer>("B3");

        shotResult = ai.Shoot();
        AssertHitShot<Submarine>("C3");

        shotResult = ai.Shoot();
        AssertSunkShot<Destroyer>("B4");

        shotResult = ai.Shoot();
        AssertMissShot("D3");

        shotResult = ai.Shoot();
        AssertHitShot<Submarine>("C4");

        shotResult = ai.Shoot();
        AssertSunkShot<Submarine>("C5");
    }

    [Test]
    public void AIPlayer_ShouldContinueToShootRandomly_AfterSunkShot()
    {
        playerBoard = new BoardBuilder()
            .AddVerticalShip(new Destroyer())
                .AtPosition("B3")
            .Build();

        BindSeededAIPlayer(001);

        shotResult = ai.Shoot();
        AssertHitShot<Destroyer>("B3");

        shotResult = ai.Shoot();
        AssertSunkShot<Destroyer>("C3");

        shotResult = ai.Shoot();
        shotResult.tile.notation.Should().NotBe("D3");
    }

    private void BindSeededAIPlayer(int seed)
    {
        player = new Player(playerBoard);
        ai = new AIPlayer(seed);

        player.board.Bind(ai.board);
    }

    private void AssertMissShot(string tileStr)
    {
        shotResult.tile.notation.Should().Be(tileStr);
        shotResult.hit.Should().BeFalse();
    }

    private void AssertHitShot<T>(string tileStr)
    {
        shotResult.tile.notation.Should().Be(tileStr);
        shotResult.hit.Should().BeTrue();
        shotResult.ship.Should().BeAssignableTo<T>();
    }

    private void AssertSunkShot<T>(string tileStr)
    {
        shotResult.tile.notation.Should().Be(tileStr);
        shotResult.hit.Should().BeTrue();
        shotResult.ship.Should().BeAssignableTo<T>();
        shotResult.sunk.Should().BeTrue();
    }

    private static TestCaseData[] cases =
    {
        new TestCaseData(001, "B3"),
        new TestCaseData(002, "E8"),
        new TestCaseData(003, "G3")
    };
}