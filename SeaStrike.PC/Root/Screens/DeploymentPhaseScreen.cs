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
        startGameButton = new StartGameButton(game, boardBuilder)
        {
            Top = -10,
            Visible = false,
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom
        };
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        AddBackButton();
        AddPhaseLabel();
        AddHelpButton();
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

    private void AddBackButton()
    {
        mainGrid.Widgets.Add(new BackButton(game)
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        });
    }

    private void AddPhaseLabel()
    {
        mainGrid.Widgets.Add(new Label()
        {
            Text = SeaStrike.stringStorage.deploymentPhaseScreenTitle,
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center
        });
    }

    private void AddHelpButton()
    {
        mainGrid.Widgets.Add(new HelpButton(game)
        {
            Text = SeaStrike.stringStorage.helpButtonLabel,
            Width = 40,
            Height = 40,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            helpWindowContent = SeaStrike.stringStorage.dpHelpWindowContent
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

    private void AddStartGameButton() => mainGrid.Widgets.Add(startGameButton);

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