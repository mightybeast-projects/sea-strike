using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class ShipTests
{
    [TestCaseSource(nameof(shipInitializationCases))]
    public void ShipInitialization_IsCorrect(Ship ship, int shipWidth)
    {
        ship.name.Should().Be(ship.GetType().Name);
        ship.width.Should().Be(shipWidth);
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
    }

    [TestCaseSource(nameof(shipToStringCases))]
    public void Ship_ToString_IsCorrect(Ship ship, string result) =>
        ship.ToString().Should().Be(result);

    private static TestCaseData[] shipInitializationCases =
    {
        new TestCaseData(new Destroyer(), 2),
        new TestCaseData(new Cruiser(), 3),
        new TestCaseData(new Submarine(), 3),
        new TestCaseData(new Battleship(), 4),
        new TestCaseData(new Carrier(), 5)
    };

    private static TestCaseData[] shipToStringCases =
    {
        new TestCaseData(new Destroyer(), "Destroyer (Width : 2)."),
        new TestCaseData(new Cruiser(), "Cruiser (Width : 3)."),
        new TestCaseData(new Submarine(), "Submarine (Width : 3)."),
        new TestCaseData(new Battleship(), "Battleship (Width : 4)."),
        new TestCaseData(new Carrier(), "Carrier (Width : 5).")
    };
}