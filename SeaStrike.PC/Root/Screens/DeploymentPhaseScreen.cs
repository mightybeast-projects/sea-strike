using System.Linq;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.GridTile;
using SeaStrike.PC.Root.Widgets.Modal;

using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class DeploymentPhaseScreen : SeaStrikeScreen
{
    protected BoardBuilder boardBuilder => player.boardBuilder;

    private Grid mainGrid;

    public DeploymentPhaseScreen(SeaStrikePlayer player) : base(player) =>
        player.boardBuilder = new BoardBuilder();

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

        seaStrikeGame.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime) =>
        UpdateStartButtonVisibility();

    private GameButton BackButton => new GameButton(OnBackButtonPressed)
    {
        Text = SeaStrikeGame.stringStorage.backButtonLabel,
        Width = 40,
        Height = 40,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
    };

    private Label PhaseLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.deploymentPhaseScreenTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrikeGame.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private GameButton HelpButton =>
        new GameButton(() =>
            ShowHelpWindow(SeaStrikeGame.stringStorage.dpHelpWindowContent))
        {
            Width = 40,
            Height = 40,
            Text = SeaStrikeGame.stringStorage.helpButtonLabel,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top
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

    private GameButton StartGameButton => new GameButton(OnStartButtonPressed)
    {
        Text = SeaStrikeGame.stringStorage.startGameButtonLabel,
        Top = -10,
        Visible = false,
        GridRow = 1,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Bottom
    };

    protected virtual void OnBackButtonPressed() =>
        seaStrikeGame.screenManager.LoadScreen(new MainMenuScreen(player));

    protected virtual void OnStartButtonPressed() =>
        player.RedirectToBattleScreen();

    private void ShowHelpWindow(string[] content) =>
        new HelpWindow(content).ShowModal(seaStrikeGame.desktop);

    private void ShowShipAdditionDialog(object obj)
    {
        if (boardBuilder.shipsPool.Length == 0)
            return;

        Tile tile = ((EmptyGridTileButton)obj).tile;

        new ShipAdditionWindow(tile.notation, boardBuilder)
            .ShowModal(seaStrikeGame.desktop);
    }

    private void RemoveShip(object obj) =>
        boardBuilder.RemoveShipAt(((AllyShipGridTileButton)obj).tile.notation);

    private void UpdateStartButtonVisibility() =>
        mainGrid.Widgets.Last().Visible =
            boardBuilder.shipsPool.Length == 0 ? true : false;
}