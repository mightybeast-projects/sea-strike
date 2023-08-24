using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class PlayerTests
{
    private Board playerBoard;
    private Board opponentBoard;
    private Player player;
    private Player opponent;

    [SetUp]
    public void SetUp()
    {
        playerBoard = new BoardBuilder().Build();
        opponentBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
            .AtPosition("A2")
            .Build();

        playerBoard.Bind(opponentBoard);

        player = new Player(playerBoard);
        opponent = new Player(opponentBoard);
    }

    [Test]
    public void PlayerInitialization_IsCorrect() =>
        player.board.Should().Be(playerBoard);

    [Test]
    public void Player_CanShoot()
    {
        ShootResult result = player.Shoot("A1");

        opponentBoard.oceanGrid.GetTile("A1").hasBeenHit.Should().BeTrue();
        result.tile.Should().Be(opponentBoard.oceanGrid.tiles[0, 0]);
        result.hit.Should().BeFalse();
        result.ship.Should().BeNull();
        result.sunk.Should().BeNull();
        result.ToString().Should().Be("A1 : Miss.");
    }

    [Test]
    public void PlayerShot_ReturnsNull_OnAlreadyShotTile()
    {
        player.Shoot("A1");

        player.Shoot("A1").Should().BeNull();
    }

    [Test]
    public void Player_HitAShip()
    {
        ShootResult result = player.Shoot("A2");

        opponentBoard.oceanGrid.GetTile("A2").hasBeenHit.Should().BeTrue();
        result.tile.Should().Be(opponentBoard.oceanGrid.tiles[1, 0]);
        result.hit.Should().BeTrue();
        result.ship.Should().Be(opponentBoard.ships[0]);
        result.sunk.Should().BeFalse();
        result.ToString().Should().Be("A2 : Hit. Destroyer (Width : 2).");
    }

    [Test]
    public void Player_SunkAShip()
    {
        player.Shoot("A2");
        ShootResult result = player.Shoot("A3");

        opponentBoard.oceanGrid.GetTile("A2").hasBeenHit.Should().BeTrue();
        opponentBoard.oceanGrid.GetTile("A3").hasBeenHit.Should().BeTrue();
        result.tile.Should().Be(opponentBoard.oceanGrid.tiles[2, 0]);
        result.hit.Should().BeTrue();
        result.ship.Should().Be(opponentBoard.ships[0]);
        result.sunk.Should().BeTrue();
        result.ToString().Should().Be("A3 : Hit. Destroyer (Width : 2). Sunk.");
    }
}