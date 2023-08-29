using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;

namespace SeaStrike.PC.Root.Screens;

public class MainMenuScreen : GameScreen
{
    private readonly SeaStrike game;
    private Panel mainPanel;

    public MainMenuScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        mainPanel = new Panel();

        AddTitleLabel();
        AddGameModesPanel();

        game.desktop.Root = mainPanel;
    }

    private void AddTitleLabel()
    {
        mainPanel.Widgets.Add(new Label()
        {
            Text = "SEA STRIKE",
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(56),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Top = 100,
        });
    }

    private void AddGameModesPanel()
    {
        VerticalStackPanel panel = new VerticalStackPanel()
        {
            Spacing = 20,
            Top = -100,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        panel.Widgets.Add(new Label()
        {
            Text = "Choose game mode :",
            Font = SeaStrike.fontSystem.GetFont(30),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        GameButton singlePlayerButton = new GameButton()
        {
            Text = "Single player"
        };
        singlePlayerButton.TouchUp += (s, a) =>
            game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));

        panel.Widgets.Add(singlePlayerButton);

        GameButton multiplayerButton = new GameButton()
        {
            Text = "Local multiplayer"
        };

        panel.Widgets.Add(multiplayerButton);

        mainPanel.Widgets.Add(panel);
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}