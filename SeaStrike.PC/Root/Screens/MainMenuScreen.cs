using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Screens;

public class MainMenuScreen : GameScreen
{
    private readonly SeaStrike game;

    public MainMenuScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        Panel panel = new();

        Label logo = new()
        {
            Text = "SEA STRIKE",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(56),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Top = 100,
        };
        Label hint = new()
        {
            Text = "Press any key to start...",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(32),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            Top = -100
        };

        panel.Widgets.Add(logo);
        panel.Widgets.Add(hint);

        game.desktop.Root = panel;
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        game.desktop.Render();
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().GetPressedKeyCount() > 0)
            game.screenManager.LoadScreen(new SeaStrikeGameScreen(game));
    }
}