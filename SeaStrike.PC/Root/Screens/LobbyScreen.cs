using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : GameScreen
{
    public LobbyScreen(SeaStrike game) : base(game)
    {
        game.desktop.Root = new Label() { Text = "Lobby" };
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}