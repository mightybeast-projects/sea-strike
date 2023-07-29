using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class GameTests
{
    [Test]
    public void GameInitialization_IsCorrect()
    {
        Board player1Board = new BoardBuilder().Build();
        Board player2Board = new BoardBuilder().Build();

        Game game = new Game(player1Board, player2Board);

        player1Board.targetGrid.Should().Be(player2Board.oceanGrid);
        game.player1.board.Should().Be(player1Board);
        game.player2.board.Should().Be(player2Board);
        game.currentPlayer.Should().Be(game.player1);
    }
}