using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.GridTile;

public abstract class GridTileButton : ImageButton
{
    public readonly Tile tile;

    protected abstract Color textureColor { get; }

    protected GridTileButton(Tile tile, Action<object> onClick)
    {
        this.tile = tile;

        Texture2D texture = new Texture2D(MyraEnvironment.GraphicsDevice, 1, 1);

        texture.SetData(new[] { textureColor });

        Image = new TextureRegion(texture, new Rectangle(0, 0, 15, 15));
        GridColumn = tile.i + 1;
        GridRow = tile.j + 1;
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;

        TouchUp += (s, a) => onClick?.Invoke(s);
    }
}