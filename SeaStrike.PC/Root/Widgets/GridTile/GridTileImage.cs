using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.GridTile;

public abstract class GridTileImage : Image
{
    protected abstract Color textureColor { get; }

    protected GridTileImage(Tile tile)
    {
        Texture2D texture = new Texture2D(MyraEnvironment.GraphicsDevice, 1, 1);

        texture.SetData(new[] { textureColor });

        Renderable = new TextureRegion(texture, new Rectangle(0, 0, 20, 20));
        GridColumn = tile.i + 1;
        GridRow = tile.j + 1;
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;
        Top = 1;
        Left = 1;
    }
}