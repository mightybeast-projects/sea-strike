using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.GameCore.Root.Widgets;

namespace SeaStrike.GameCore.Root.Screens;

public class MainMenuScreen : SeaStrikeScreen
{
    public MainMenuScreen(SeaStrikeGame seaStrikeGame)
        : base(new SeaStrikePlayer(seaStrikeGame)) { }
    public MainMenuScreen(SeaStrikePlayer player) : base(player) { }

    public override void LoadContent()
    {
        base.LoadContent();

        SeaStrikeGame.audioManager.PlayMainOST();

        Panel mainPanel = new Panel();

        mainPanel.Widgets.Add(TitleLabel);
        mainPanel.Widgets.Add(GameModesPanel);

        seaStrikeGame.desktop.Root = mainPanel;
    }

    private Label TitleLabel => new Label()
    {
        Top = 100,
        Text = SeaStrikeGame.stringStorage.gameTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrikeGame.fontSystem.GetFont(56),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Top,
    };

    private GameModesPanel GameModesPanel => new GameModesPanel(player)
    {
        Top = 50,
        VerticalAlignment = VerticalAlignment.Center
    };
}