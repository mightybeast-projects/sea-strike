using Microsoft.Xna.Framework;
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

        mainPanel.Widgets.Add(TitleLabel);
        mainPanel.Widgets.Add(new GameModesPanel(game));

        game.desktop.Root = mainPanel;
    }

    private Label TitleLabel => new Label()
    {
        Text = SeaStrike.stringStorage.gameTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrike.fontSystem.GetFont(56),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Top,
        Top = 100,
    };

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}