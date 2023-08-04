using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.UI;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeGameScreen : GameScreen
{
    private readonly SeaStrike game;
    private Grid mainGrid;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Magenta
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        mainGrid.ColumnsProportions.Add(new Proportion(ProportionType.Pixels, 300));

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

        mainGrid.AddChild(new BoardPanel(game)
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
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