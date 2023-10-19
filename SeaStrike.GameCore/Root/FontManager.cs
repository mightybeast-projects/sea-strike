using FontStashSharp;
using Microsoft.Xna.Framework;

namespace SeaStrike.GameCore.Root;

public class FontManager
{
    private readonly FontSystem fontSystem;

    public FontManager() => fontSystem = new FontSystem();

    public void LoadFont()
    {
        string path = SeaStrikeGame.stringStorage.fontPath;
        byte[] ttf;

        using (var stream = TitleContainer.OpenStream(path))
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                ttf = ms.ToArray();
            }
        }

        fontSystem.AddFont(ttf);
    }

    public DynamicSpriteFont GetFont(int fontSize) =>
        fontSystem.GetFont(fontSize);
}