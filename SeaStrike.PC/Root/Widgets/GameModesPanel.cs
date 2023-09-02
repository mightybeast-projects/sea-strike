using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class GameModesPanel : VerticalStackPanel
{
    private SeaStrike game;

    public GameModesPanel(SeaStrike game)
    {
        this.game = game;

        Spacing = 20;

        Widgets.Add(GameModeLabel);
        Widgets.Add(SinglePlayerButton);
        Widgets.Add(MultiplayerButton);
    }

    private Label GameModeLabel => new Label()
    {
        Text = SeaStrike.stringStorage.gameModeLabel,
        Font = SeaStrike.fontSystem.GetFont(30),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private GameButton SinglePlayerButton =>
        new GameButton(() => LoadSinglePlayerMode())
        {
            Text = SeaStrike.stringStorage.singlePlayerButtonLabel
        };

    private GameButton MultiplayerButton =>
        new GameButton(() => LoadMultiplayerMode())
        {
            Text = SeaStrike.stringStorage.multiplayerButonLabel
        };

    private void LoadSinglePlayerMode() =>
        game.screenManager.LoadScreen(new DeploymentPhaseScreen(game));

    private void LoadMultiplayerMode() =>
        game.screenManager.LoadScreen(new LobbyScreen(game));
}