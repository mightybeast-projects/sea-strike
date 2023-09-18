using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root.Widgets;

public class GameModesPanel : VerticalStackPanel
{
    private readonly SeaStrikePlayer player;

    public GameModesPanel(SeaStrikePlayer player)
    {
        this.player = player;

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
        new GameButton(player.RedirectTo<DeploymentPhaseScreen>)
        {
            Text = SeaStrikeGame.stringStorage.singlePlayerButtonLabel
        };

    private GameButton MultiplayerButton =>
        new GameButton(player.RedirectTo<LobbyScreen>)
        {
            Text = SeaStrikeGame.stringStorage.multiplayerButonLabel
        };
}