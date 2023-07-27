using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Exceptions;

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
        board.opponentBoard.Should().BeNull();
        board.targetGrid.Should().BeNull();
        board.ships.Should().NotBeNull().And.BeEmpty();
    }

    [Test]
    public void Board_CanAdd_NewHorizontalShip()
    {
        Ship ship = new Cruiser();
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
    public void Board_CanAdd_NewVerticalShip()
    {
        Ship ship = new Cruiser();
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
    public void Board_Throws_CannotFitShipException()
    {
        board.Invoking(b => b.AddHorizontalShip(new Battleship(), "A8"))
            .Should().Throw<CannotFitShipException>();
        board.Invoking(b => b.AddVerticalShip(new Battleship(), "H1"))
            .Should().Throw<CannotFitShipException>();
    }

    [Test]
    public void Board_Throws_TileIsOccupiedByOtherShipException()
    {
        board.AddHorizontalShip(new Cruiser(), "A1");

        board.Invoking(b => b.AddHorizontalShip(new Battleship(), "A1"))
            .Should().Throw<TileIsOccupiedByOtherShipException>();
        board.Invoking(b => b.AddVerticalShip(new Battleship(), "A1"))
            .Should().Throw<TileIsOccupiedByOtherShipException>();
    }

    [Test]
    public void Board_CanRemoveShip()
    {
        Ship ship = new Cruiser();

        board.AddHorizontalShip(ship, "A1");

        Tile[] occupiedTiles = ship.occupiedTiles;

        board.RemoveShipAt("A3");

        board.ships.Should().BeEmpty();
        occupiedTiles.Should().OnlyContain(tile => !tile.isOccupied);
    }

    [Test]
    public void Board_DoesNotThrow_OnRemoveShip_IfTileIsNotOccupied() =>
        board.Invoking(b => b.RemoveShipAt("A2"))
        .Should().NotThrow();

    [Test]
    public void Board_CanBindOpponentBoard()
    {
        Board player1Board = new Board();
        Board player2Board = new Board();

        player1Board.Bind(player2Board);

        player1Board.opponentBoard.Should().BeEquivalentTo(player2Board);
        player1Board.targetGrid.Should().BeEquivalentTo(player2Board.oceanGrid);
        player2Board.opponentBoard.Should().BeEquivalentTo(player1Board);
        player2Board.targetGrid.Should().BeEquivalentTo(player1Board.oceanGrid);
    }
}