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
    private Board board;
    private Grid mainGrid;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        board = new BoardBuilder().Build();

        mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Magenta
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center
        });

        mainGrid.Widgets.Add(new GridPanel(game, board.oceanGrid)
        {
            GridRow = 1,
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
        new ShipAdditionDialog((TextButton)sender, board)
        .ShowModal(game.desktop);
}