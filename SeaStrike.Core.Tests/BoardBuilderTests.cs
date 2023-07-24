using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class BoardBuilderTests
{
    [Test]
    public void BuilderInitialization_IsCorrect() =>
        new BoardBuilder().board.Should().BeNull();

    [Test]
    public void BuilderInit_IsCorrect()
    {
        BoardBuilder builder = new BoardBuilder().Init();

        builder.Should().NotBeNull();
        builder.board.Should().NotBeNull();

    }

    [Test]
    public void Builder_CanAdd_NewHorizontalShips()
    {
        Ship ship1 = new Ship(3);
        Ship ship2 = new Ship(5);

        BoardBuilder builder =
            new BoardBuilder().Init()
            .AddHorizontalShip(ship1, "A1")
            .AddHorizontalShip(ship2, "J1");

        builder.board.ships.Should().Contain(ship1).And.Contain(ship2);
    }

    [Test]
    public void Builder_CanAdd_NewVerticalShips()
    {
        Ship ship1 = new Ship(3);
        Ship ship2 = new Ship(5);

        BoardBuilder builder =
            new BoardBuilder().Init()
            .AddVerticalShip(ship1, "A1")
            .AddVerticalShip(ship2, "A3");

        builder.board.ships.Should().Contain(ship1).And.Contain(ship2);
    }
}