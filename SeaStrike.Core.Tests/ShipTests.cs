using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class ShipTests
{
    [Test]
    public void ShipInitialization_IsCorrect()
    {
        Ship ship = new Ship(2);

        ship.width.Should().Be(2);
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
    }
}