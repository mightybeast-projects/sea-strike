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

    public MainMenuScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        font32 = Content.Load<SpriteFont>("font/font32");
        font18 = Content.Load<SpriteFont>("font/font18");
    }

    public override void Draw(GameTime gameTime)
    {
        game.spriteBatch.Begin();

        Vector2 logoV = font32.MeasureString(logo);
        Vector2 hintV = font18.MeasureString(hint);
        Rectangle bounds = game.Window.ClientBounds;
        Vector2 logoPosition = new Vector2(bounds.Width / 2 - logoV.X / 2, 100);
        Vector2 hintPosition = new Vector2(bounds.Width / 2 - hintV.X / 2, bounds.Height - hintV.Y * 3);

        game.spriteBatch.DrawString(font32, logo, logoPosition, Color.White);
        game.spriteBatch.DrawString(font18, hint, hintPosition, Color.White);

        game.spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        if (state.GetPressedKeyCount() > 0)
            game.screenManager.LoadScreen(new SeaStrikeGameScreen(game));
    }
}