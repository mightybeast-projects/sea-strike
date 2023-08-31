using System;
using Microsoft.Xna.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.GridTile;

public class AllyShipGridTileButton : GridTileButton
{
    protected override Color textureColor => Color.LawnGreen;

    public AllyShipGridTileButton(Tile tile, Action<object> onClick)
        : base(tile, onClick) { }
}