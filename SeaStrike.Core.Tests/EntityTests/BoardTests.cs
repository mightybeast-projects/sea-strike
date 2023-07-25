using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class BoardTests
{
    private Board board;

    [SetUp]
    public void SetUp() => board = new Board();

    [Test]
    public void BoardInitialization_IsCorrect()
    {
        board.oceanGrid.Should().NotBeNull();
        board.targetGrid.Should().NotBeNull();
        board.ships.Should().NotBeNull().And.BeEmpty();
    }

    [Test]
    public void Board_CanAdd_NewHorizontalShip()
    {
        Ship ship = new Ship(3);
        List<Tile> occupiedTiles = new List<Tile>()
        {
            board.oceanGrid.GetTile("A1"),
            board.oceanGrid.GetTile("A2"),
            board.oceanGrid.GetTile("A3")
        };

        board.AddHorizontalShip(ship, "A1");

        board.ships.Should().Contain(ship);
        ship.occupiedTiles.Should().BeEquivalentTo(occupiedTiles);
        occupiedTiles.Should().OnlyContain(tile => tile.occupiedBy == ship);
    }

    [Test]
    public void Board_CannotAdd_NewHorizontalShip()
    {
        Ship ship = new Ship(4);
        List<Tile> nonOccupiedTiles = new List<Tile>()
        {
            board.oceanGrid.GetTile("A8"),
            board.oceanGrid.GetTile("A9"),
            board.oceanGrid.GetTile("A10")
        };

        board.AddHorizontalShip(ship, "A8");

        board.ships.Should().NotContain(ship);
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
        nonOccupiedTiles.Should().OnlyContain(tile => tile.isOccupied == false);
    }

    [Test]
    public void Board_CanAdd_NewVerticalShip()
    {
        Ship ship = new Ship(3);
        List<Tile> occupiedTiles = new List<Tile>()
        {
            board.oceanGrid.GetTile("A1"),
            board.oceanGrid.GetTile("B1"),
            board.oceanGrid.GetTile("C1")
        };

        board.AddVerticalShip(ship, "A1");

        board.ships.Should().Contain(ship);
        ship.occupiedTiles.Should().BeEquivalentTo(occupiedTiles);
        occupiedTiles.Should().OnlyContain(tile => tile.occupiedBy == ship);
    }

    [Test]
    public void Board_CannotAdd_NewVerticalShip()
    {
        Ship ship = new Ship(4);
        List<Tile> nonOccupiedTiles = new List<Tile>()
        {
            board.oceanGrid.GetTile("H1"),
            board.oceanGrid.GetTile("I1"),
            board.oceanGrid.GetTile("J1")
        };

        board.AddVerticalShip(ship, "H1");

        board.ships.Should().NotContain(ship);
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
        nonOccupiedTiles.Should().OnlyContain(tile => tile.isOccupied == false);
    }
}