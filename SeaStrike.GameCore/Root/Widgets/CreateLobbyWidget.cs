using FontStashSharp.RichText;
using Myra.Graphics2D.UI;
using SeaStrike.GameCore.Root.Network;

namespace SeaStrike.GameCore.Root.Widgets;

public class CreateLobbyWidget : VerticalStackPanel
{
    private readonly NetPlayer player;

    public CreateLobbyWidget(NetPlayer player)
    {
        this.player = player;

        Spacing = 5;

        Widgets.Add(CreateLobbyButton);
        Widgets.Add(WaitForLobbyCreationLabel);
    }

    private GameButton CreateLobbyButton => new GameButton(CreateNewLobby)
    {
        Text = SeaStrikeGame.stringStorage.createLobbyButtonLabel
    };

    private Label CreatedNewLobbyLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.createdLobbyLabel,
        TextAlign = TextHorizontalAlignment.Center
    };

    private Label WaitForLobbyCreationLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.waitForLobbyCreationLabel,
        TextAlign = TextHorizontalAlignment.Center
    };

    private void CreateNewLobby()
    {
        player.CreateServer();

        Widgets.Clear();
        Widgets.Add(CreatedNewLobbyLabel);
    }
}