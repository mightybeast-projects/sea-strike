using Microsoft.Xna.Framework;
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

        Panel mainPanel = new Panel();

        mainPanel.Widgets.Add(new Label()
        {
            Text = "SEA STRIKE",
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(56),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Top = 100,
        });

        mainPanel.Widgets.Add(new Label()
        {
            Text = "Press any key to start...",
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(32),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            Top = -100
        });

        game.desktop.Root = mainPanel;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().GetPressedKeyCount() > 0)
            game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));
    }
}