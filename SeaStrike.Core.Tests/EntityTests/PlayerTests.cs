using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class PlayerTests
{
    [Test]
    public void PlayerInitialization_IsCorrect()
    {
        Board board = new BoardBuilder().Build();

        Player player = new Player(board);

        player.board.Should().Be(board);
    }

    [Test]
    public void Player_CanShoot()
    {
        Board player1Board = new BoardBuilder().Build();
        Board player2Board = new BoardBuilder().Build();

        player1Board.Bind(player2Board);

        Player player1 = new Player(player1Board);
        Player player2 = new Player(player2Board);

        player1.Shoot("A1");

        player2Board.oceanGrid.GetTile("A1").hasBeenHit.Should().BeTrue();
    }
}