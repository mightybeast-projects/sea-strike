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

        AddPhaseLabel();
        AddGridButtonsPanel();
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

    private void AddPhaseLabel()
    {
        mainGrid.Widgets.Add(new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center
        });
    }

    private void AddGridButtonsPanel() =>
        mainGrid.Widgets.Add(new GridButtonsPanel(boardBuilder));

    private void AddOceanGridPanel()
    {
        oceanGridPanel = new GridPanel(boardBuilder.Build().oceanGrid, true)
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
        startGameButton = new GameButton()
        {
            Top = -10,
            Text = "Start game",
            Visible = false,
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom
        };
        startGameButton.TouchUp += (s, a) =>
            game.screenManager.LoadScreen(
                new BattlePhaseScreen(game, boardBuilder.Build()));

        mainGrid.Widgets.Add(startGameButton);
    }

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
}