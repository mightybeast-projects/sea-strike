using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class GameTests
{
    private Board player1Board;
    private Board player2Board;
    private Game game;

    [SetUp]
    public void SetUp()
    {
        player1Board = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("A1")
            .Build();
        player2Board = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("A1")
            .Build();

        game = new Game(player1Board, player2Board);
    }

    [Test]
    public void OnePlayerGameInitialization_IsCorrect()
    {
        player1Board = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        game = new Game(player1Board);

        player1Board.targetGrid.Should().Be(game.player2.board.oceanGrid);
        game.player1.board.Should().Be(player1Board);
        game.player2.board.ships.Count.Should().Be(5);
        game.currentPlayer.Should().Be(game.player1);
        game.player2.Should().BeAssignableTo<AIPlayer>();
        game.isOver.Should().BeFalse();
    }

    [Test]
    public void TwoPlayerGameInitialization_IsCorrect()
    {
        player1Board.targetGrid.Should().Be(player2Board.oceanGrid);
        game.player1.board.Should().Be(player1Board);
        game.player2.board.Should().Be(player2Board);
        game.currentPlayer.Should().Be(game.player1);
        game.isOver.Should().BeFalse();
    }

    [Test]
    public void Game_CanHandle_CurrentPlayerShot()
    {
        ShootResult result = game.HandleShot("A1");

        result.hit.Should().BeTrue();
        game.currentPlayer.Should().Be(game.player2);
    }

    [Test]
    public void Game_IsOver_WhenOneOfThePlayers_SunkAllShips()
    {
        game.HandleShot("A1");
        game.HandleShot("A1");
        game.HandleShot("A2");

        game.isOver.Should().BeTrue();
    }

    [Test]
    public void Game_ReturnsNullOnShot_IfGameIsOver()
    {
        game.HandleShot("A1");
        game.HandleShot("A1");
        game.HandleShot("A2");

        game.HandleShot("A3").Should().BeNull();
    }
}