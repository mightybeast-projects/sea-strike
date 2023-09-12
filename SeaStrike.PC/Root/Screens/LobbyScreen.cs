using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Network;

namespace SeaStrike.PC.Root.Screens;

public class LobbyScreen : SeaStrikeScreen
{
    private NetPlayer player;
    private Grid mainGrid;

    public LobbyScreen(SeaStrike game) : base(game)
    {
        player = new NetPlayer(game);
        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(BackButton);
        mainGrid.Widgets.Add(ScreenTitleLabel);
        mainGrid.Widgets.Add(LobbyButtonsPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime) =>
        player.UpdateNetManagers();

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

    private void OnBackButtonPressed()
    {
        player.Disconnect();

        game.screenManager.LoadScreen(new MainMenuScreen(game));
    }
}