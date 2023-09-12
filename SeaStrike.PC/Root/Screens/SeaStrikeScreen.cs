using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeScreen : GameScreen
{
    protected SeaStrike game;

    public SeaStrikeScreen(SeaStrike game) : base(game) => this.game = game;

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}