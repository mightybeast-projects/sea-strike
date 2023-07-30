using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeGameScreen : GameScreen
{
    private SeaStrike game;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.SkyBlue);
    }

    public override void Update(GameTime gameTime)
    {
    }
}