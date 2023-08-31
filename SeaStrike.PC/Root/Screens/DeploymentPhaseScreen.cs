using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
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

    private BackButton BackButton => new BackButton(game)
    {
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
            OnOccupiedTileClicked = RemoveShip
        };

    private StartGameButton StartGameButton =>
        new StartGameButton(game, boardBuilder)
        {
            Top = -10,
            Visible = false,
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom
        };

    private void ShowShipAdditionDialog(object sender)
    {
        if (boardBuilder.shipsPool.Length == 0)
            return;

        string tilePosition = ((TextButton)sender).Text;
        ShipAdditionWindow dialog =
            new ShipAdditionWindow(tilePosition, boardBuilder);
        dialog.ShowModal(game.desktop);
    }

    private void RemoveShip(object sender)
    {
        GridTileImageButton occupiedTileButton = (GridTileImageButton)sender;
        boardBuilder.RemoveShipAt(occupiedTileButton.tile.notation);
    }

    private void UpdateStartButtonVisibility() =>
        mainGrid.Widgets.Last().Visible =
            boardBuilder.shipsPool.Length == 0 ? true : false;
}