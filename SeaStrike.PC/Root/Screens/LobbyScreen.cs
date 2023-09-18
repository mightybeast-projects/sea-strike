using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.Modal;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : SeaStrikeScreen
{
    private new NetPlayer player;

    public LobbyScreen(SeaStrikePlayer player) : base(player)
    {
        this.player = new NetPlayer(seaStrikeGame);

        Grid mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(BackButton);
        mainGrid.Widgets.Add(ScreenTitleLabel);
        mainGrid.Widgets.Add(LobbyButtonsPanel);

        seaStrikeGame.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime) =>
        player.UpdateNetManagers();

    private Label ScreenTitleLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.lobbyScreenLabel,
        TextColor = Color.LawnGreen,
        Font = SeaStrikeGame.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private GameButton BackButton => new GameButton(OnBackButtonPressed)
    {
        Text = SeaStrikeGame.stringStorage.backButtonLabel,
        Width = 40,
        Height = 40,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
    };

    private LobbyButtonsPanel LobbyButtonsPanel => new LobbyButtonsPanel(player)
    {
        GridRow = 1,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
    };

    private void OnBackButtonPressed()
    {
        if (player.isHost)
            new DisconnectionWarningWindow(player.Disconnect)
                .ShowModal(seaStrikeGame.desktop);
        else
            player.RedirectTo<MainMenuScreen>();
    }
}