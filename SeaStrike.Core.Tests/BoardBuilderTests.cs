using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class BoardBuilderTests
{
    [Test]
    public void BuilderInitialization_IsCorrect() =>
        new BoardBuilder().board.Should().NotBeNull();

    [Test]
    public void Builder_CanAdd_NewHorizontalShips()
    {
        Board board =
            new BoardBuilder()
            .AddHorizontalShip(new Ship(3))
                .AtPosition("A1")
            .AddHorizontalShip(new Ship(5))
                .AtPosition("J1")
            .Build();

        board.ships.Count.Should().Be(2);
    }

    [Test]
    public void Builder_CanAdd_NewVerticalShips()
    {
        Board board =
            new BoardBuilder()
            .AddVerticalShip(new Ship(3))
                .AtPosition("A1")
            .AddVerticalShip(new Ship(5))
                .AtPosition("A3")
            .Build();

        board.ships.Count.Should().Be(2);
    }
}