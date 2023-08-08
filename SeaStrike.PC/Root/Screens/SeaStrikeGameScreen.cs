using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.UI;
using SeaStrikeGame = SeaStrike.Core.Entity.Game;
using Grid = Myra.Graphics2D.UI.Grid;
using System;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeGameScreen : GameScreen
{
    private readonly SeaStrike game;
    private Grid mainGrid;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        Board board1 = new BoardBuilder()
            .AddVerticalShip(new Carrier())
                .AtPosition("A2")
            .Build();

        Board board2 = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        SeaStrikeGame ssGame = new SeaStrikeGame(board1, board2);

        ssGame.HandleShot("A1");
        ssGame.HandleShot("A1");
        ssGame.HandleShot("A2");
        ssGame.HandleShot("A2");
        ssGame.HandleShot("A3");
        ssGame.HandleShot("A3");

        mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Magenta
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        mainGrid.ColumnsProportions.Add(new Proportion(ProportionType.Pixels, 280));

        mainGrid.Widgets.Add(new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            GridColumnSpan = 2,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        mainGrid.Widgets.Add(new ShipsPanel(game)
        {
            GridRow = 1,
            VerticalAlignment = VerticalAlignment.Center
        });

        mainGrid.Widgets.Add(new GridPanel(game, board1.oceanGrid)
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            OnEmptyTileClicked = InitializeShipAdditionDialog
        });

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        game.desktop.Render();
    }

    public override void Update(GameTime gameTime) { }

    private void InitializeShipAdditionDialog(object sender, EventArgs args) =>
        new ShipAdditionDialog(game, (TextButton)sender)
        .ShowModal(game.desktop);
}