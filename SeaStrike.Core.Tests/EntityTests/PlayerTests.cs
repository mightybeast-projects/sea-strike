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
}