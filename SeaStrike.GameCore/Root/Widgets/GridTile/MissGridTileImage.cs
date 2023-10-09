using Microsoft.Xna.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.GameCore.Root.Widgets.GridTile;

public class MissGridTileImage : GridTileImage
{
    protected override Color textureColor => Color.White;

    public MissGridTileImage(Tile tile) : base(tile) { }
}