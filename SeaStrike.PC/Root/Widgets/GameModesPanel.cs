using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class GameModesPanel : VerticalStackPanel
{
    public GameModesPanel(SeaStrike game)
    {
        Spacing = 20;
        Top = -100;
        VerticalAlignment = VerticalAlignment.Bottom;

        Widgets.Add(GameModeLabel);

        GameButton singlePlayerButton = new GameButton()
        {
            Text = SeaStrike.stringStorage.singlePlayerButtonLabel
        };
        singlePlayerButton.TouchUp += (s, a) =>
            game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));

        Widgets.Add(singlePlayerButton);

        GameButton multiplayerButton = new GameButton(null)
        {
            Text = SeaStrike.stringStorage.multiplayerButonLabel
        };

        Widgets.Add(multiplayerButton);
    }

    private Label GameModeLabel => new Label()
    {
        Text = SeaStrike.stringStorage.gameModeLabel,
        Font = SeaStrike.fontSystem.GetFont(30),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center
    };
}