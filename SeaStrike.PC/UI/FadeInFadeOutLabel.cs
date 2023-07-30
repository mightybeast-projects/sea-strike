using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeaStrike.PC.UI;

public class FadeInFadeOutLabel : Label
{
    private int alphaValue = 70;
    private int fadeIncrement = 3;

    public FadeInFadeOutLabel(string str, SpriteFont font) : base(str, font) { }

    public override void Draw(SpriteBatch sb)
    {
        sb.GraphicsDevice.Clear(Color.Black);

        base.Draw(sb);
    }

    public void Update()
    {
        alphaValue += fadeIncrement;

        if (alphaValue >= 255 || alphaValue <= 70)
            fadeIncrement *= -1;

        color = new Color(color.R, color.G, color.B, alphaValue);
    }
}