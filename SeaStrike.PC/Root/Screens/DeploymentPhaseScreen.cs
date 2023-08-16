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

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        AddTitleLabel();
        AddButtonsPanel();
        AddOceanGridPanel();
        AddStartGameButton();

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
            HorizontalAlignment = HorizontalAlignment.Center
        });
    }

    private void AddButtonsPanel()
    {
        HorizontalStackPanel buttonsPanel = new HorizontalStackPanel()
        {
            Top = 10,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
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

        mainGrid.Widgets.Add(buttonsPanel);
    }

    private void AddOceanGridPanel()
    {
        oceanGridPanel = new GridPanel(game, boardBuilder.Build().oceanGrid)
        {
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            OnEmptyTileClicked = ShowShipAdditionDialog,
            OnOccupiedTileClicked = RemoveShip
        };
        boardBuilder.Subscribe(oceanGridPanel);

        mainGrid.Widgets.Add(oceanGridPanel);
    }

    private void AddStartGameButton()
    {
        startGameButton = new GameButton(game)
        {
            Top = -10,
            Text = "Start game",
            Visible = false,
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        mainGrid.Widgets.Add(startGameButton);
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