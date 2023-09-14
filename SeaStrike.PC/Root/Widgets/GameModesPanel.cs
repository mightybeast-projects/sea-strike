using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class GameModesPanel : VerticalStackPanel
{
    private SeaStrikeGame seaStrikeGame;

    public GameModesPanel(SeaStrikeGame seaStrikeGame)
    {
        this.seaStrikeGame = seaStrikeGame;

        Spacing = 20;

        Widgets.Add(GameModeLabel);
        Widgets.Add(SinglePlayerButton);
        Widgets.Add(MultiplayerButton);
    }

    private Label GameModeLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.gameModeLabel,
        Font = SeaStrikeGame.fontSystem.GetFont(30),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private GameButton SinglePlayerButton =>
        new GameButton(() => LoadSinglePlayerMode())
        {
            Text = SeaStrikeGame.stringStorage.singlePlayerButtonLabel
        };

    private GameButton MultiplayerButton =>
        new GameButton(() => LoadMultiplayerMode())
        {
            Text = SeaStrikeGame.stringStorage.multiplayerButonLabel
        };

    private void LoadSinglePlayerMode() =>
        seaStrikeGame.screenManager.LoadScreen(new DeploymentPhaseScreen(seaStrikeGame));

    private void LoadMultiplayerMode() =>
        seaStrikeGame.screenManager.LoadScreen(new LobbyScreen(seaStrikeGame));
}