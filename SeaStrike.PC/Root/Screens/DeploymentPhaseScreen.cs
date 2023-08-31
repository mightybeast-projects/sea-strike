using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.Button;
using SeaStrike.PC.Root.Widgets.GridTile;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class DeploymentPhaseScreen : GameScreen
{
    private readonly SeaStrike game;
    private readonly BoardBuilder boardBuilder;
    private Grid mainGrid;

    public DeploymentPhaseScreen(SeaStrike game) : base(game)
    {
        this.game = game;

        boardBuilder = new BoardBuilder();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(BackButton);
        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(GridButtonsPanel);
        mainGrid.Widgets.Add(OceanGridPanel);
        mainGrid.Widgets.Add(StartGameButton);

        game.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime) =>
        UpdateStartButtonVisibility();

    public override void Draw(GameTime gameTime) { }

    private GameButton BackButton => new GameButton(ReturnOnMainMenuScreen)
    {
        Text = SeaStrike.stringStorage.backButtonLabel,
        Width = 40,
        Height = 40,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
    };

    private Label PhaseLabel => new Label()
    {
        Text = SeaStrike.stringStorage.deploymentPhaseScreenTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrike.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private HelpButton HelpButton => new HelpButton(game)
    {
        Text = SeaStrike.stringStorage.helpButtonLabel,
        Width = 40,
        Height = 40,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        helpWindowContent = SeaStrike.stringStorage.dpHelpWindowContent
    };

    private GridButtonsPanel GridButtonsPanel =>
        new GridButtonsPanel(boardBuilder);

    private GridPanel OceanGridPanel =>
        new GridPanel(boardBuilder.Build(), true)
        {
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            OnEmptyTileClicked = ShowShipAdditionDialog,
            OnAllyShipClicked = RemoveShip
        };

    private GameButton StartGameButton => new GameButton(LoadBattlePhaseScreen)
    {
        Text = SeaStrike.stringStorage.startGameButtonLabel,
        Top = -10,
        Visible = false,
        GridRow = 1,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Bottom
    };

    private void ReturnOnMainMenuScreen() =>
        game.screenManager.LoadScreen(new MainMenuScreen(game));

    private void LoadBattlePhaseScreen() =>
        game.screenManager
            .LoadScreen(new BattlePhaseScreen(game, boardBuilder.Build()));

    private void ShowShipAdditionDialog(object obj)
    {
        if (boardBuilder.shipsPool.Length == 0)
            return;

        Tile tile = ((EmptyGridTileButton)obj).tile;

        new ShipAdditionWindow(tile.notation, boardBuilder)
            .ShowModal(game.desktop);
    }

    private void RemoveShip(object obj) =>
        ((AllyShipGridTileButton)obj).RemoveShip(boardBuilder);

    private void UpdateStartButtonVisibility() =>
        mainGrid.Widgets.Last().Visible =
            boardBuilder.shipsPool.Length == 0 ? true : false;
}