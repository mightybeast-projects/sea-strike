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
    private BoardBuilder boardBuilder;
    private Grid mainGrid;
    private GridPanel oceanGridPanel;

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

        AddTitleLabel();
        AddOceanGridPanel();

        game.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime) { }

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