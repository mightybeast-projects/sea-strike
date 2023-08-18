using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class PlayerTests
{
    private Board player1Board;
    private Board player2Board;
    private Player player1;
    private Player player2;

    [SetUp]
    public void SetUp()
    {
        player1Board = new BoardBuilder().Build();
        player2Board = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
            .AtPosition("A2")
            .Build();

        player1Board.Bind(player2Board);

        player1 = new Player(player1Board);
        player2 = new Player(player2Board);
    }

    [Test]
    public void PlayerInitialization_IsCorrect() =>
        player1.board.Should().Be(player1Board);

    [Test]
    public void Player_CanShoot()
    {
        ShootResult result = player1.Shoot("A1");

        player2Board.oceanGrid.GetTile("A1").hasBeenHit.Should().BeTrue();
        result.tile.Should().Be(player2Board.oceanGrid.tiles[0, 0]);
        result.hit.Should().BeFalse();
        result.ship.Should().BeNull();
        result.sunk.Should().BeNull();
        result.ToString().Should().Be("A1 : Miss.");
    }

    [Test]
    public void PlayerShot_ReturnsNull_OnAlreadyShotTile()
    {
        player1.Shoot("A1");

        player1.Shoot("A1").Should().BeNull();
    }

    [Test]
    public void Player_HitAShip()
    {
        ShootResult result = player1.Shoot("A2");

        player2Board.oceanGrid.GetTile("A2").hasBeenHit.Should().BeTrue();
        result.tile.Should().Be(player2Board.oceanGrid.tiles[1, 0]);
        result.hit.Should().BeTrue();
        result.ship.Should().Be(player2Board.ships[0]);
        result.sunk.Should().BeFalse();
        result.ToString().Should().Be("A2 : Hit. Destroyer (Width : 2).");
    }

    [Test]
    public void Player_SunkAShip()
    {
        player1.Shoot("A2");
        ShootResult result = player1.Shoot("A3");

        player2Board.oceanGrid.GetTile("A2").hasBeenHit.Should().BeTrue();
        player2Board.oceanGrid.GetTile("A3").hasBeenHit.Should().BeTrue();
        result.tile.Should().Be(player2Board.oceanGrid.tiles[2, 0]);
        result.hit.Should().BeTrue();
        result.ship.Should().Be(player2Board.ships[0]);
        result.sunk.Should().BeTrue();
        result.ToString().Should().Be("A3 : Hit. Destroyer (Width : 2). Sunk.");
    }
}