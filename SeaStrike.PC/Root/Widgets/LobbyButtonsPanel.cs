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
            Text = SeaStrike.stringStorage.createLobbyButtonLabel
        };

    private GameButton ConnectToLobbyButton =>
        new GameButton(() => ConnectToLobby())
        {
            Text = SeaStrike.stringStorage.connectToLobbyButtonLabel
        };

    private void CreateNewLobby()
    {
        player.CreateServer();

        ConnectToLobby();
    }

    private void ConnectToLobby() => player.CreateClient();
}