using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Screens;

public class MainMenuScreen : GameScreen
{
    private SeaStrike game;
    private Panel panel;
    private Label logo;
    private Label hint;

    public MainMenuScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        panel = new Panel();

        logo = new Label()
        {
            Text = "Sea strike",
            Font = game.fontSystem.GetFont(56),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Top = 100,
        };
        hint = new Label()
        {
            Text = "Press any key to start...",
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