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

        Spacing = 20;

        Widgets.Add(CreateLobbyButton);
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

    private void CreateNewLobby()
    {
        player.CreateServer();

        Widgets.Clear();
        Widgets.Add(CreatedNewLobbyLabel);
    }
}