using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class EnemyGridPanel : AllyGridPanel
{
    public EnemyGridPanel(SeaStrike game, Grid oceanGrid)
        : base(game, oceanGrid) { }

    protected override void AddGridTile(Tile tile)
    {
        if (tile.hasBeenHit)
            AddAllyGridTileImage(tile);
        else
            AddEmptyGridTileButton(tile);
    }
}
