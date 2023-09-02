using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Network;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : GameScreen
{
    private SeaStrike game;
    private SeaStrikeServer server;
    private SeaStrikeClient client;

    public LobbyScreen(SeaStrike game) : base(game)
    {
        this.game = game;

        VerticalStackPanel panel = new VerticalStackPanel()
        {
            Spacing = 10,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel.Widgets.Add(new Label()
        {
            Text = "Lobby screen",
            HorizontalAlignment = HorizontalAlignment.Center,
        });

        panel.Widgets.Add(new GameButton(() => CreateNewLobby())
        {
            Text = "Create new lobby"
        });

        panel.Widgets.Add(new GameButton(() => ConnectToLobby())
        {
            Text = "Connect to existing lobby"
        });

        game.desktop.Root = panel;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime)
    {
        if (server is not null)
            server.PollEvents();
        if (client is not null)
            client.PollEvents();
    }

    private void CreateNewLobby()
    {
        server = new SeaStrikeServer(game);

        ConnectToLobby();
    }

    private void ConnectToLobby()
    {
        client = new SeaStrikeClient(game);
    }
}