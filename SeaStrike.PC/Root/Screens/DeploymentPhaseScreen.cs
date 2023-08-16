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
    private GridPanel oceanGridPanel;
    private TextButton startGameButton;

    public DeploymentPhaseScreen(SeaStrike game) : base(game)
    {
        this.game = game;

        boardBuilder = new BoardBuilder();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Magenta
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        mainGrid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));

        AddTitleLabel();
        AddButtonsPanel();
        AddOceanGridPanel();

        game.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime)
    {
        if (boardBuilder.shipsPool.Length == 0)
            startGameButton.Visible = true;
        else
            startGameButton.Visible = false;
    }

    public override void Draw(GameTime gameTime) { }

    private void AddTitleLabel()
    {
        mainGrid.Widgets.Add(new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddButtonsPanel()
    {
        VerticalStackPanel buttonsPanel = new VerticalStackPanel()
        {
            Width = 200,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            GridRow = 1,
            Spacing = 50
        };

        TextButton randomizeButton = new GameButton(game)
        {
            Text = "Randomize"
        };
        randomizeButton.TouchUp += (s, a) =>
            boardBuilder.RandomizeShipsStartingPosition();
        buttonsPanel.Widgets.Add(randomizeButton);

        TextButton clearButton = new GameButton(game)
        {
            Text = "Clear grid"
        };
        clearButton.TouchUp += (s, a) => boardBuilder.ClearOceanGrid();
        buttonsPanel.Widgets.Add(clearButton);

        startGameButton = new GameButton(game)
        {
            Text = "Start game",
            Visible = false
        };
        buttonsPanel.Widgets.Add(startGameButton);

        mainGrid.Widgets.Add(buttonsPanel);
    }

    private void AddOceanGridPanel()
    {
        oceanGridPanel = new GridPanel(game, boardBuilder.Build().oceanGrid)
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            OnEmptyTileClicked = ShowShipAdditionDialog,
            OnOccupiedTileClicked = RemoveShip
        };
        boardBuilder.Subscribe(oceanGridPanel);

        mainGrid.Widgets.Add(oceanGridPanel);
    }

    private void ShowShipAdditionDialog(object sender)
    {
        if (boardBuilder.shipsPool.Length == 0)
            return;

        string tilePosition = ((TextButton)sender).Text;
        ShipAdditionDialog dialog =
            new ShipAdditionDialog(tilePosition, boardBuilder);
        dialog.ShowModal(game.desktop);
    }

    private void RemoveShip(object sender)
    {
        GridTileImageButton occupiedTileButton = (GridTileImageButton)sender;
        boardBuilder.RemoveShipAt(occupiedTileButton.tile.notation);
    }
}