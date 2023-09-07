using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Network;
using FontStashSharp.RichText;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : GameScreen
{
    private SeaStrike game;
    private NetPlayer player;

    public LobbyScreen(SeaStrike game) : base(game)
    {
        this.game = game;

        player = new NetPlayer(game);

        VerticalStackPanel panel = new VerticalStackPanel()
        {
            Spacing = 20,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel.Widgets.Add(ScreenTitleLabel);
        panel.Widgets.Add(CreateLobbyButton);
        panel.Widgets.Add(ConnectToLobbyButton);

        game.desktop.Root = panel;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) => player.UpdateNetManagers();

    private Label ScreenTitleLabel => new Label()
    {
        Text = SeaStrike.stringStorage.lobbyScreenLabel,
        HorizontalAlignment = HorizontalAlignment.Center,
    };

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

    private Label CreatedNewLobbyLabel => new Label()
    {
        Text = SeaStrike.stringStorage.createdLobbyLabel,
        TextAlign = TextHorizontalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
    };

    private void CreateNewLobby()
    {
        player.CreateServer();

        ConnectToLobby();

        game.desktop.Root = CreatedNewLobbyLabel;
    }

    private void ConnectToLobby() => player.CreateClient();
}