using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.UI;

public class GridTileImage : Image
{
    public GridTileImage(Tile tile, GraphicsDevice graphicsDevice)
    {
        Texture2D damagedShip = new Texture2D(graphicsDevice, 1, 1);
        Color tileColor = Color.Black;

        if (!tile.isOccupied && tile.hasBeenHit)
            tileColor = Color.White;
        else if (tile.isOccupied && tile.hasBeenHit)
            tileColor = Color.Red;
        else if (tile.isOccupied && !tile.hasBeenHit)
            tileColor = Color.LawnGreen;

        damagedShip.SetData(new[] { tileColor });

        Renderable = new TextureRegion(damagedShip, new Rectangle(0, 0, 15, 15));
        GridColumn = tile.i + 1;
        GridRow = tile.j + 1;
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;
    }
}