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
        Tile tile = new Tile(0, 3);

        Assert.AreEqual(10, grid.tiles.GetLength(0));
        Assert.AreEqual(10, grid.tiles.GetLength(1));
        Assert.AreEqual(tile.notation, grid.tiles[0, 3].notation);
    }

    [Test]
    public void Grid_CanGetTile_ByNotation()
    {
        Grid grid = new Grid();
        Tile tile = new Tile(0, 3);

        Assert.AreEqual(tile.notation, grid.GetTile("D1").notation);
    }

    private static TestCaseData[] gridTileNotationCases =
    {
        new TestCaseData("D1").Returns(new Tile(0, 3))
    };
}