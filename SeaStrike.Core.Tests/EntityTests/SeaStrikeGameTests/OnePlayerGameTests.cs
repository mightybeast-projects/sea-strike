using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.Core.Tests.EntityTests.SeaStrikeGameTests;

[TestFixture]
public class OnePlayerGameTests
{
    private Board playerBoard;
    private SeaStrikeGame game;

    [SetUp]
    public void SetUp()
    {
        playerBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        game = new SeaStrikeGame(playerBoard);
    }

    [Test]
    public void GameInitialization_IsCorrect()
    {
        playerBoard.targetGrid.Should().Be(game.opponent.board.oceanGrid);
        game.player.board.Should().Be(playerBoard);
        game.opponent.board.ships.Count.Should().Be(5);
        game.currentPlayer.Should().Be(game.player);
        game.opponent.Should().BeAssignableTo<AIPlayer>();
        game.isOver.Should().BeFalse();
    }

    [Test]
    public void Game_ShouldHandleAIPlayerShot()
    {
        game.HandleCurrentPlayerShot("A1");

        game.HandleAIPlayerShot().Should().NotBeNull();
    }
}