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
    private Grid mainGrid;

    public LobbyScreen(SeaStrike game) : base(game)
    {
        this.game = game;

        player = new NetPlayer(game);

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(BackButton);
        mainGrid.Widgets.Add(ScreenTitleLabel);
        mainGrid.Widgets.Add(LobbyButtonsPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime)
    {
        if (player.isHost)
            ShowCreatedLobbyLabel();

        player.UpdateNetManagers();
    }

    private Label ScreenTitleLabel => new Label()
    {
        Text = SeaStrike.stringStorage.lobbyScreenLabel,
        TextColor = Color.LawnGreen,
        Font = SeaStrike.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private GameButton BackButton => new GameButton(OnBackButtonPressed)
    {
        Text = SeaStrike.stringStorage.backButtonLabel,
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

    private Label CreatedNewLobbyLabel => new Label()
    {
        GridRow = 1,
        Text = SeaStrike.stringStorage.createdLobbyLabel,
        TextAlign = TextHorizontalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
    };

    private void OnBackButtonPressed()
    {
        player.Disconnect();
        game.screenManager.LoadScreen(new MainMenuScreen(game));
    }

    private void ShowCreatedLobbyLabel()
    {
        mainGrid.Widgets.RemoveAt(2);
        mainGrid.Widgets.Add(CreatedNewLobbyLabel);
    }
}