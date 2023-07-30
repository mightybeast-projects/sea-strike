using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Screens;

namespace SeaStrike.PC.Root.Screens;

public class MainMenuScreen : GameScreen
{
    private SeaStrike game;
    private SpriteFont font32;
    private SpriteFont font18;
    private string logo = "Sea Strike";
    private string hint = "Press any button to begin";
    private Vector2 logoPosition;
    private Vector2 hintPosition;
    private int alphaValue = 70;
    private int fadeIncrement = 3;

    public MainMenuScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        font32 = Content.Load<SpriteFont>("font/font48");
        font18 = Content.Load<SpriteFont>("font/font18");

        Vector2 logoV = font32.MeasureString(logo);
        Vector2 hintV = font18.MeasureString(hint);
        Rectangle bounds = game.Window.ClientBounds;
        logoPosition = new Vector2(bounds.Width / 2 - logoV.X / 2, 100);
        hintPosition = new Vector2(bounds.Width / 2 - hintV.X / 2, bounds.Height - hintV.Y * 3);
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

        game.spriteBatch.DrawString(font32, logo, logoPosition, Color.White);
        game.spriteBatch.DrawString(
            font18, hint, hintPosition, new Color(255, 255, 255, alphaValue)
        );

        game.spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().GetPressedKeyCount() > 0)
            game.screenManager.LoadScreen(new SeaStrikeGameScreen(game));

        alphaValue += fadeIncrement;

        if (alphaValue >= 255 || alphaValue <= 70)
            fadeIncrement *= -1;
    }
}