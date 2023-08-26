using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.Core.Tests.EntityTests.SeaStrikeGameTests;

[TestFixture]
public class AIPlayerTests
{
    private Player player;

    [SetUp]
    public void SetUp()
    {
        Board playerBoard = new BoardBuilder()
            .AddHorizontalShip(new Destroyer())
                .AtPosition("D4")
            .Build();

        player = new Player(playerBoard);
    }

    [TestCaseSource(nameof(cases))]
    public void SeededShoot_ShouldChooseExpectedTile(
        int seed,
        string expectedTileStr)
    {
        AIPlayer opponent = new AIPlayer(seed);

        player.board.Bind(opponent.board);

        opponent.Shoot();

        player.board.oceanGrid.GetTile(expectedTileStr).hasBeenHit
            .Should().BeTrue();
    }

    private static TestCaseData[] cases =
    {
        new TestCaseData(001, "B3"),
        new TestCaseData(002, "E8"),
        new TestCaseData(003, "G3")
    };
}