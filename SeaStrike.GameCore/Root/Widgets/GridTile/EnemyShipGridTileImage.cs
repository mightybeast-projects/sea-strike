using Microsoft.Xna.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.GameCore.Root.Widgets.GridTile;

public class EnemyShipGridTileImage : GridTileImage
{
    protected override Color textureColor => Color.Red;

    public EnemyShipGridTileImage(Tile tile) : base(tile) { }
}