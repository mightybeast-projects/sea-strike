using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class GridTests
{
    [Test]
    public void GridInitialization() => new Grid();

    [Test]
    public void Grid_IsTilesMatrix()
    {
        Grid grid = new Grid();

        grid.tiles.GetLength(0).Should().Be(10);
        grid.tiles.GetLength(1).Should().Be(10);
        grid.tiles[0, 3].Should().BeEquivalentTo(new Tile(0, 3), options =>
            options.IncludingInternalFields()
        );
    }

    [TestCaseSource(nameof(gridTileNotationCases))]
    public void Grid_CanGetTile_ByNotation(string notation, Tile tile) =>
        new Grid().GetTile(notation).Should().BeEquivalentTo(tile,
            options => options.IncludingInternalFields()
        );

    private static TestCaseData[] gridTileNotationCases =
    {
        new TestCaseData("A1", new Tile(0, 0)),
        new TestCaseData("D1", new Tile(0, 3)),
        new TestCaseData("E8", new Tile(7, 4)),
    };
}