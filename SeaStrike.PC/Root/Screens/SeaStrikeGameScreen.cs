using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

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

        AddPhaseLabel();
        AddShipsPanel();
        AddBoardPanel();

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        game.desktop.Render();
    }

    public override void Update(GameTime gameTime) { }

    private void AddPhaseLabel()
    {
        Label phaseLabel = new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            GridColumnSpan = 2,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        mainGrid.Widgets.Add(phaseLabel);
    }

    private void AddShipsPanel()
    {
        Panel shipsPanel = new Panel()
        {
            Width = 200,
            Height = 300,
            GridRow = 1,
            Border = new SolidBrush(Color.LawnGreen),
            BorderThickness = new Thickness(1),
            VerticalAlignment = VerticalAlignment.Center
        };

        mainGrid.Widgets.Add(shipsPanel);
    }

    private void AddBoardPanel()
    {
        Panel boardPanel = new Panel()
        {
            Width = 345,
            Height = 345,
            GridRow = 1,
            GridColumn = 1,
            Border = new SolidBrush(Color.LawnGreen),
            BorderThickness = new Thickness(2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        Grid boardGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Green
        };

        for (int i = 0; i < 12; i++)
        {
            boardGrid.RowsProportions.Add(new Proportion(ProportionType.Pixels, (float)boardPanel.Width / 11));
            boardGrid.ColumnsProportions.Add(new Proportion(ProportionType.Pixels, (float)boardPanel.Height / 11));
        }

        for (int i = 1; i < 11; i++)
        {
            Label label = new Label()
            {
                Text = i.ToString(),
                Font = game.fontSystem.GetFont(24),
                GridColumn = i,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            boardGrid.Widgets.Add(label);
        }

        for (int i = 1; i < 11; i++)
        {
            Label label = new Label()
            {
                Text = ((char)(i + 64)).ToString(),
                Font = game.fontSystem.GetFont(24),
                GridRow = i,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            boardGrid.Widgets.Add(label);
        }

        boardPanel.AddChild(boardGrid);

        mainGrid.Widgets.Add(boardPanel);
    }
}