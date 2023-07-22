using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class TileTests
{
    [Test]
    public void TileInitialization() => new Tile(0, 0);

    [TestCaseSource(nameof(tileCoordinatesCases))]
    public void TileCoordinates_AreCorrect(int i, int j)
    {
        Tile tile = new Tile(i, j);

        Assert.AreEqual(i, tile.i);
        Assert.AreEqual(j, tile.j);
    }

    private static TestCaseData[] tileCoordinatesCases =
    {
        new TestCaseData(0, 0),
        new TestCaseData(0, 1),
        new TestCaseData(1, 0)
    };
}