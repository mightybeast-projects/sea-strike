using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class ShipTests
{
    [TestCaseSource(nameof(cases))]
    public void ShipInitialization_IsCorrect(Ship ship, int shipWidth)
    {
        ship.name.Should().Be(ship.GetType().Name);
        ship.width.Should().Be(shipWidth);
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
    }

    private static TestCaseData[] cases =
    {
        new TestCaseData(new Destroyer(), 2),
        new TestCaseData(new Cruiser(), 3),
        new TestCaseData(new Submarine(), 3),
        new TestCaseData(new Battleship(), 4),
        new TestCaseData(new Carrier(), 5)
    };
}