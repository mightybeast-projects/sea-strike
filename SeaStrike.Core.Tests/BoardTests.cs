using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class BoardTests
{
    [Test]
    public void BoardInitialization_IsCorrect()
    {
        Board board = new Board();

        board.oceanGrid.Should().NotBeNull();
        board.targetGrid.Should().NotBeNull();
        board.ships.Should().NotBeNull().And.BeEmpty();
    }

    [Test]
    public void Board_CanAdd_NewHorizontalShip()
    {
        Board board = new Board();
        Ship ship = new Ship(3);
        List<Tile> occupiedTiles = new List<Tile>()
        {
            board.oceanGrid.GetTile("A1"),
            board.oceanGrid.GetTile("A2"),
            board.oceanGrid.GetTile("A3")
        };

        board.AddHorizontalShip(ship, "A1");

        board.ships.Should().Contain(ship);
        occupiedTiles.ForEach(tile => TileShouldBeOccupiedBy(tile, ship));
        ship.occupiedTiles.Should().BeEquivalentTo(occupiedTiles);
    }

    [Test]
    public void Board_CannotAdd_NewHorizontalShip()
    {
        Board board = new Board();
        Ship ship = new Ship(4);

        board.AddHorizontalShip(ship, "A8");

        board.ships.Should().NotContain(ship);
        board.oceanGrid.GetTile("A8").isOccupied.Should().BeFalse();
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
    }

    [Test]
    public void Board_CanAdd_NewVerticalShip()
    {
        Board board = new Board();
        Ship ship = new Ship(3);
        List<Tile> occupiedTiles = new List<Tile>()
        {
            board.oceanGrid.GetTile("A1"),
            board.oceanGrid.GetTile("B1"),
            board.oceanGrid.GetTile("C1")
        };

        board.AddVerticalShip(ship, "A1");

        board.ships.Should().Contain(ship);
        occupiedTiles.ForEach(tile => TileShouldBeOccupiedBy(tile, ship));
        ship.occupiedTiles.Should().BeEquivalentTo(occupiedTiles);
    }

    [Test]
    public void Board_CannotAdd_NewVerticalShip()
    {
        Board board = new Board();
        Ship ship = new Ship(4);

        board.AddVerticalShip(ship, "H1");

        board.ships.Should().NotContain(ship);
        board.oceanGrid.GetTile("H1").isOccupied.Should().BeFalse();
        ship.occupiedTiles.Should().OnlyContain(tile => tile == null);
    }

    private void TileShouldBeOccupiedBy(Tile tile, Ship ship)
    {
        tile.isOccupied.Should().BeTrue();
        tile.occupiedBy.Should().BeEquivalentTo(ship);
    }
}