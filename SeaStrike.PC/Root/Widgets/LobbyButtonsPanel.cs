using System.Threading.Tasks;
using FontStashSharp.RichText;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Network;

namespace SeaStrike.PC.Root.Widgets;

public class LobbyButtonsPanel : VerticalStackPanel
{
    private NetPlayer player;

    public LobbyButtonsPanel(NetPlayer player)
    {
        this.player = player;

        Spacing = 20;

        Widgets.Add(CreateLobbyButton);
        Widgets.Add(ConnectToLobbyButton);
    }

    private GameButton CreateLobbyButton =>
        new GameButton(() => CreateNewLobby())
        {
            Text = SeaStrikeGame.stringStorage.createLobbyButtonLabel
        };

    private GameButton ConnectToLobbyButton =>
        new GameButton(() => ConnectToLobby())
        {
            Text = SeaStrikeGame.stringStorage.connectToLobbyButtonLabel
        };

    private Label CreatedNewLobbyLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.createdLobbyLabel,
        TextAlign = TextHorizontalAlignment.Center
    };

    private Label CannotDiscoverAnyLobbyLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.cannotDiscoverAnyLobbyLabel,
        TextAlign = TextHorizontalAlignment.Center,
    };

    private void CreateNewLobby()
    {
        player.CreateServer();

        ConnectToLobby();

        Widgets.Clear();
        Widgets.Add(CreatedNewLobbyLabel);
    }

    private void ConnectToLobby()
    {
        player.CreateClient();

        Task.Delay(1000).ContinueWith(t => ShowFailedDiscoveryLabel());
    }

    private void ShowFailedDiscoveryLabel()
    {
        if (!player.isHost && Widgets.Count != 3)
            Widgets.Add(CannotDiscoverAnyLobbyLabel);
    }
}