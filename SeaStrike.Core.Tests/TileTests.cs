using FluentAssertions;
using NUnit.Framework;

namespace SeaStrike.Core.Tests;

[TestFixture]
public class TileTests
{
    [TestCaseSource(nameof(tileCoordinatesCases))]
    public void TileCoordinates_AreCorrect(int i, int j)
    {
        Tile tile = new Tile(i, j);

        tile.i.Should().Be(i);
        tile.j.Should().Be(j);
    }

    [TestCaseSource(nameof(tileNotationCases))]
    public string TileNotation_IsCorrect(int i, int j) =>
        new Tile(i, j).notation;

    private static TestCaseData[] tileCoordinatesCases =
    {
        new TestCaseData(0, 0),
        new TestCaseData(0, 1),
        new TestCaseData(1, 0)
    };

    private static TestCaseData[] tileNotationCases =
    {
        new TestCaseData(0, 0).Returns("A1"),
        new TestCaseData(0, 1).Returns("B1"),
        new TestCaseData(1, 0).Returns("A2")
    };
}