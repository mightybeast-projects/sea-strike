using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeaStrike.PC.UI;

public class Label
{
    public string str { get; private set; }
    public SpriteFont font { get; private set; }
    public Vector2 position { get; private set; }
    public Vector2 origin { get; private set; }
    public Color color { get; protected set; }

    public Label(string str, SpriteFont font)
    {
        this.str = str;
        this.font = font;
        origin = font.MeasureString(str);
    }

    public virtual void Draw(SpriteBatch sb) =>
        sb.DrawString(font, str, position, color);

    public Label SetPosition(Vector2 position)
    {
        this.position = position;

        return this;
    }

    public Label SetColor(Color color)
    {
        this.color = color;

        return this;
    }
}