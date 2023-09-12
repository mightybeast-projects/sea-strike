using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;

namespace SeaStrike.PC.Root.Screens;

public class MainMenuScreen : SeaStrikeScreen
{
    public MainMenuScreen(SeaStrike game) : base(game) { }

    public override void LoadContent()
    {
        base.LoadContent();

        Panel mainPanel = new Panel();

        mainPanel.Widgets.Add(TitleLabel);
        mainPanel.Widgets.Add(GameModesPanel);

        game.desktop.Root = mainPanel;
    }

    private Label TitleLabel => new Label()
    {
        Top = 100,
        Text = SeaStrike.stringStorage.gameTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrike.fontSystem.GetFont(56),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Top,
    };

    private GameModesPanel GameModesPanel => new GameModesPanel(game)
    {
        Top = -100,
        VerticalAlignment = VerticalAlignment.Bottom
    };
}