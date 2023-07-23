using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class BoardTests
{
    [Test]
    public void BoardInitialization()
    {
        Board board = new Board();

        board.oceanGrid.Should().NotBeNull();
        board.targetGrid.Should().NotBeNull();
    }
}