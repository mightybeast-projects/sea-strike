using System;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class EmptyGridTileButton : TextButton
{
    public EmptyGridTileButton(Tile tile, Action<object> onClick = null)
    {
        Text = tile.notation;
        Opacity = 0.1f;
        Font = SeaStrike.fontSystem.GetFont(18);
        GridColumn = tile.i + 1;
        GridRow = tile.j + 1;
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;

        TouchUp += (s, a) => onClick?.Invoke(s);
    }
}