using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.UI;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeGameScreen : GameScreen
{
    private readonly SeaStrike game;
    private Grid mainGrid;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        Board board = new BoardBuilder().Build();

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

        mainGrid.Widgets.Add(new GridPanel(game, board.oceanGrid)
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        });

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        game.desktop.Render();
    }

    public override void Update(GameTime gameTime) { }
}