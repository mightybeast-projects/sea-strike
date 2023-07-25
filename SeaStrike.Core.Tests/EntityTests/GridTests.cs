using FluentAssertions;
using NUnit.Framework;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Exceptions;

namespace SeaStrike.Core.Tests.EntityTests;

[TestFixture]
public class GridTests
{
    [Test]
    public void GridInitialization_IsCorrect()
    {
        Grid grid = new Grid();

        grid.tiles.GetLength(0).Should().Be(10);
        grid.tiles.GetLength(1).Should().Be(10);
        grid.tiles[0, 3].Should().BeEquivalentTo(new Tile(0, 3), options =>
            options.IncludingInternalFields()
        );
    }

    [TestCaseSource(nameof(correctGridTileNotationCases))]
    public void Grid_CanGetTile_ByNotation(string notation, Tile tile) =>
        new Grid().GetTile(notation).Should().BeEquivalentTo(tile,
            options => options.IncludingInternalFields()
        );

    [TestCaseSource(nameof(incorrectGridTileNotationCases))]
    public void Grid_Throws_CannotFindSpecifiedTileException(string notation) =>
        new Grid()
        .Invoking(g => g.GetTile(notation))
        .Should().Throw<CannotFindSpecifiedTileException>();

    private static TestCaseData[] correctGridTileNotationCases =
    {
        new TestCaseData("A1", new Tile(0, 0)),
        new TestCaseData("D1", new Tile(0, 3)),
        new TestCaseData("E8", new Tile(7, 4)),
    };

    private static TestCaseData[] incorrectGridTileNotationCases =
    {
        new TestCaseData("a1"),
        new TestCaseData("z0"),
        new TestCaseData("  ")
    };
}