using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.GameCore.Root.Network;
using SeaStrike.GameCore.Root.Widgets;
using SeaStrike.GameCore.Root.Widgets.Modal;

namespace SeaStrike.GameCore.Root.Screens.Multiplayer;

public class LobbyScreen : SeaStrikeScreen
{
    private new NetPlayer player => (NetPlayer)base.player;

    public LobbyScreen(SeaStrikePlayer player)
        : base(new NetPlayer(player.seaStrikeGame))
    {
        Grid mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(BackButton);
        mainGrid.Widgets.Add(ScreenTitleLabel);
        mainGrid.Widgets.Add(CreateLobbyWidget);

        seaStrikeGame.desktop.Root = mainGrid;

        this.player.CreateClient();
    }

    public override void Update(GameTime gameTime)
    {
        player.DiscoverServer();
        player.UpdateNetManagers();
    }

    private Label ScreenTitleLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.lobbyScreenLabel,
        TextColor = Color.LawnGreen,
        Font = SeaStrikeGame.fontManager.GetFont(40),
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

    private CreateLobbyWidget CreateLobbyWidget => new CreateLobbyWidget(player)
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