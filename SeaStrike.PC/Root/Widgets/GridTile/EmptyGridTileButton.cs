using System;
using Microsoft.Xna.Framework;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.GridTile;

public class EmptyGridTileButton : GridTileButton
{
    protected override Color textureColor => Color.Black;

    public EmptyGridTileButton(Tile tile, Action<object> onClick)
        : base(tile, onClick) { }
}