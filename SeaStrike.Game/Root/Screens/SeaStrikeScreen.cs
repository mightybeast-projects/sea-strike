using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeScreen : GameScreen
{
    protected SeaStrikePlayer player;
    protected SeaStrikeGame seaStrikeGame => player.seaStrikeGame;

    public SeaStrikeScreen(SeaStrikePlayer player)
        : base(player.seaStrikeGame) => this.player = player;

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}