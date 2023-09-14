using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeScreen : GameScreen
{
    protected SeaStrikeGame seaStrikeGame;

    public SeaStrikeScreen(SeaStrikeGame seaStrikeGame) : base(seaStrikeGame)
        => this.seaStrikeGame = seaStrikeGame;

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}