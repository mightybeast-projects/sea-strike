using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SeaStrike.PC.UI;

namespace SeaStrike.PC.Root.Screens;

public class MainMenuScreen : GameScreen
{
    private SeaStrike game;
    private SpriteFont font48;
    private SpriteFont font18;
    private Label logo;
    private FadeInFadeOutLabel hint;

    public MainMenuScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        font48 = Content.Load<SpriteFont>("font/font48");
        font18 = Content.Load<SpriteFont>("font/font18");

        Rectangle bounds = game.Window.ClientBounds;

        logo = new Label("Sea strike", font48);
        hint = new FadeInFadeOutLabel("Press any button to start", font18);

        logo.SetPosition(new Vector2(bounds.Width / 2 - logo.origin.X / 2, 100))
            .SetColor(Color.White);
        hint.SetPosition(
                new Vector2(bounds.Width / 2 - hint.origin.X / 2,
                    bounds.Height - hint.origin.Y * 3)
                )
            .SetColor(Color.White);
    }

    public override void Draw(GameTime gameTime)
    {
        game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

        logo.Draw(game.spriteBatch);
        hint.Draw(game.spriteBatch);

        game.spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().GetPressedKeyCount() > 0)
            game.screenManager.LoadScreen(new SeaStrikeGameScreen(game));

        hint.Update();
    }
}