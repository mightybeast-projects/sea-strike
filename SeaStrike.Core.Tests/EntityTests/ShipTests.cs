using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class ShipTests
{
    private Ship ship;

    [Test]
    public void DestroyerInitialization_IsCorrect()
    {
        ship = new Destroyer();

        AssertShip("Destroyer", 2);
    }

    [Test]
    public void CruiserInitialization_IsCorrect()
    {
        ship = new Cruiser();

        AssertShip("Cruiser", 3);
    }

    [Test]
    public void SubmarineInitialization_IsCorrect()
    {
        ship = new Submarine();

        AssertShip("Submarine", 3);
    }

    [Test]
    public void BattleshipInitialization_IsCorrect()
    {
        ship = new Battleship();

        AssertShip("Battleship", 4);
    }

    [Test]
    public void CarrierInitialization_IsCorrect()
    {
        ship = new Carrier();

        AssertShip("Carrier", 5);
    }

    private void AssertShip(string shipName, int shipWidth)
    {
        ship.name.Should().Be(shipName);
        ship.width.Should().Be(shipWidth);
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
    }
}