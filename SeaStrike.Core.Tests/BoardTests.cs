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

        board.AddHorizontalShip(ship, occupiedTiles[0]);

        board.ships.Should().Contain(ship);
        occupiedTiles.ForEach(tile => TileShouldBeOccupiedBy(tile, ship));
        ship.occupiedTiles.Should().BeEquivalentTo(occupiedTiles);
    }

    private void TileShouldBeOccupiedBy(Tile tile, Ship ship)
    {
        tile.isOccupied.Should().BeTrue();
        tile.occupiedBy.Should().BeEquivalentTo(ship);
    }
}