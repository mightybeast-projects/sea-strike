using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class BoardBuilderTests
{
    [Test]
    public void BuilderInitialization_IsCorrect() =>
        new BoardBuilder().Build().Should().BeEquivalentTo(new Board());

    [Test]
    public void Builder_CanBeInitialized_WithExistingBoard()
    {
        Board board = new BoardBuilder().Build();
        new BoardBuilder(board).Build().Should().BeEquivalentTo(board);
    }

    [Test]
    public void Builder_CanRandomizeShipsPosition()
    {
        Board board = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        board.ships.Count.Should().Be(5);
    }

    [Test]
    public void Builder_CanAdd_NewHorizontalShips()
    {
        Board board = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("A1")
            .AddHorizontalShip(new Destroyer())
                .AtPosition("B1")
            .Build();

        board.ships.Count.Should().Be(2);
    }

    [Test]
    public void Builder_CanAdd_NewVerticalShips()
    {
        Board board = new BoardBuilder()
            .AddVerticalShip(new Destroyer())
                .AtPosition("A1")
            .AddVerticalShip(new Destroyer())
                .AtPosition("A3")
            .Build();

        board.ships.Count.Should().Be(2);
    }

    [Test]
    public void Builder_CanRemoveShipAtPosition()
    {
        Board board = new BoardBuilder()
            .AddHorizontalShip(new Cruiser())
                .AtPosition("A1")
            .RemoveShipAt("A2")
            .Build();

        board.ships.Should().BeEmpty();
    }
}