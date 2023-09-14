using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.Core.Entity.GameLogic.Utility;

namespace SeaStrike.Core.Tests.EntityTests.GameLogicTests;

[TestFixture]
public class TwoPlayerGameTests
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
    public void GameInitialization_IsCorrect()
    {
        playerBoard.targetGrid.Should().Be(opponentBoard.oceanGrid);
        game.player.board.Should().Be(playerBoard);
        game.opponent.board.Should().Be(opponentBoard);
        game.currentPlayer.Should().Be(game.player);
        game.isOver.Should().BeFalse();
    }

    [Test]
    public void Game_CanBeInitialized_WithOpponent_AsCurrentPlayer()
    {
        game = new Game(playerBoard, opponentBoard, true);

        game.currentPlayer.Should().Be(game.opponent);
    }

    [Test]
    public void Game_CanHandle_CurrentPlayerShot()
    {
        ShotResult result = game.HandleCurrentPlayerShot("A1");

        result.hit.Should().BeTrue();
    }

    [Test]
    public void Game_ShouldSwitchCurrentPlayer_OnCurrentPlayerShot()
    {
        game.HandleCurrentPlayerShot("A1");

        game.currentPlayer.Should().Be(game.opponent);
    }

    [Test]
    public void Game_IsOver_WhenOneOfThePlayers_SunkAllShips()
    {
        game.HandleCurrentPlayerShot("A1");
        game.HandleCurrentPlayerShot("A1");
        game.HandleCurrentPlayerShot("A2");

        game.isOver.Should().BeTrue();
    }

    [Test]
    public void Game_ReturnsNullOnShot_IfGameIsOver()
    {
        game.HandleCurrentPlayerShot("A1");
        game.HandleCurrentPlayerShot("A1");
        game.HandleCurrentPlayerShot("A2");

        game.HandleCurrentPlayerShot("A3").Should().BeNull();
    }

    [Test]
    public void Game_ShouldReturnNull_OnHandleAIPlayerShot()
    {
        game.HandleAIPlayerShot().Should().BeNull();
        game.currentPlayer.Should().Be(game.player);
    }
}