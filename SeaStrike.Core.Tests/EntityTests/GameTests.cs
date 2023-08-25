using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class GameTests
{
    private Board playerBoard;
    private Board opponentBoard;
    private Game game;

    [SetUp]
    public void SetUp()
    {
        playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("A1")
            .Build();
        opponentBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("A1")
            .Build();

        game = new Game(playerBoard, opponentBoard);
    }

    [Test]
    public void OnePlayerGameInitialization_IsCorrect()
    {
        playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        game = new Game(playerBoard);

        playerBoard.targetGrid.Should().Be(game.opponent.board.oceanGrid);
        game.player.board.Should().Be(playerBoard);
        game.opponent.board.ships.Count.Should().Be(5);
        game.currentPlayer.Should().Be(game.player);
        game.opponent.Should().BeAssignableTo<AIPlayer>();
        game.isOver.Should().BeFalse();
    }

    [Test]
    public void TwoPlayerGameInitialization_IsCorrect()
    {
        playerBoard.targetGrid.Should().Be(opponentBoard.oceanGrid);
        game.player.board.Should().Be(playerBoard);
        game.opponent.board.Should().Be(opponentBoard);
        game.currentPlayer.Should().Be(game.player);
        game.isOver.Should().BeFalse();
    }

    [Test]
    public void Game_CanHandle_CurrentPlayerShot()
    {
        ShootResult result = game.HandleShot("A1");

        result.hit.Should().BeTrue();
        game.currentPlayer.Should().Be(game.opponent);
    }

    [Test]
    public void TwoPlayerGame_ShouldSwitchCurrentPlayer_OnCurrentPlayerShot()
    {
        game.HandleShot("A1");

        game.currentPlayer.Should().Be(game.opponent);
    }

    [Test]
    public void OnePlayerGame_ShouldNotSwitchCurrentPlayer_OnPlayerShot()
    {
        playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        game = new Game(playerBoard);

        game.HandleShot("A1");

        game.currentPlayer.Should().Be(game.player);
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