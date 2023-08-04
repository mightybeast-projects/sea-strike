using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeGameScreen : GameScreen
{
    private readonly SeaStrike game;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        Grid mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Magenta
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        mainGrid.ColumnsProportions.Add(new Proportion(ProportionType.Pixels, 300));

        Label phaseLabel = new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            GridColumnSpan = 2,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        Panel shipsPanel = new Panel()
        {
            Width = 200,
            Height = 300,
            GridRow = 1,
            Background = new SolidBrush(Color.Red),
            VerticalAlignment = VerticalAlignment.Center
        };
        Panel boardPanel = new Panel()
        {
            Width = 350,
            Height = 350,
            GridRow = 1,
            GridColumn = 1,
            Background = new SolidBrush(Color.Red),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        mainGrid.Widgets.Add(phaseLabel);
        mainGrid.Widgets.Add(shipsPanel);
        mainGrid.Widgets.Add(boardPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        game.desktop.Render();
    }

    public override void Update(GameTime gameTime)
    {
    }
}