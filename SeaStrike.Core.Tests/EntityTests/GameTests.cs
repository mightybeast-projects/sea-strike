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
        player1Board = new BoardBuilder().Build();
        player2Board = new BoardBuilder().Build();
        game = new Game(player1Board, player2Board);
    }

    [Test]
    public void GameInitialization_IsCorrect()
    {
        player1Board.targetGrid.Should().Be(player2Board.oceanGrid);
        game.player1.board.Should().Be(player1Board);
        game.player2.board.Should().Be(player2Board);
        game.currentPlayer.Should().Be(game.player1);
    }

    [Test]
    public void Game_CanHandle_CurrentPlayerShot()
    {
        game.HandleShot("A1");

        player2Board.oceanGrid.GetTile("A1").hasBeenHit.Should().BeTrue();
        game.currentPlayer.Should().Be(game.player2);
    }
}