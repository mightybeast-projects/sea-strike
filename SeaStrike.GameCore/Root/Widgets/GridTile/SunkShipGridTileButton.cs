using Microsoft.Xna.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.GameCore.Root.Widgets.GridTile;

public class SunkShipGridTileButton : GridTileImage
{
    protected override Color textureColor => Color.DarkRed;

    public SunkShipGridTileButton(Tile tile) : base(tile) { }
}