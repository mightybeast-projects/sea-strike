using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
using Grid = Myra.Graphics2D.UI.Grid;
using SeaStrikeGame = SeaStrike.Core.Entity.Game;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : GameScreen
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGame seaStrikeGame;
    private readonly Board playerBoard;
    private Grid mainGrid;
    private Label resultLabel;

    public BattlePhaseScreen(SeaStrike game, Board playerBoard)
        : base(game)
    {
        this.game = game;
        this.playerBoard = playerBoard;

        Board opponentBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard);
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        AddPhaseLabel();
        AddOceanGridPanel();
        AddTargetGridPanel();

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }

    private void AddPhaseLabel()
    {
        mainGrid.Widgets.Add(new Label()
        {
            Text = "Battle phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddOceanGridPanel()
    {
        VerticalStackPanel verticalPanel = new VerticalStackPanel()
        {
            GridRow = 1,
            Spacing = 10
        };

        verticalPanel.Widgets.Add(new Label()
        {
            Text = "Your's ocean grid : ",
            Font = game.fontSystem.GetFont(28),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        GridPanel oceanGridPanel =
            new GridPanel(game, playerBoard.oceanGrid, true)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        playerBoard.Subscribe(oceanGridPanel);

        verticalPanel.Widgets.Add(oceanGridPanel);

        mainGrid.Widgets.Add(verticalPanel);
    }

    private void AddTargetGridPanel()
    {
        VerticalStackPanel verticalPanel = new VerticalStackPanel()
        {
            GridRow = 1,
            GridColumn = 1,
            Spacing = 10
        };

        verticalPanel.Widgets.Add(new Label()
        {
            Text = "Opponent's ocean grid : ",
            Font = game.fontSystem.GetFont(28),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        GridPanel targetGridPanel =
            new GridPanel(game, playerBoard.targetGrid, false)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                OnEmptyTileClicked = ShootTile
            };
        playerBoard.opponentBoard.Subscribe(targetGridPanel);

        verticalPanel.Widgets.Add(targetGridPanel);

        resultLabel = new Label()
        {
            Font = game.fontSystem.GetFont(28),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        verticalPanel.Widgets.Add(resultLabel);

        mainGrid.Widgets.Add(verticalPanel);
    }

    private void ShootTile(object sender)
    {
        string tileStr = ((TextButton)sender).Text;

        ShootResult result = seaStrikeGame.HandleShot(tileStr);

        if (seaStrikeGame.isOver)
            ShowVictoryScreen();

        resultLabel.Text = result.ToString();

        seaStrikeGame.HandleShot(tileStr);

        if (seaStrikeGame.isOver)
            ShowLostScreen();
    }

    private void ShowVictoryScreen() => ShowGameOverWindow("You won!");

    private void ShowLostScreen() => ShowGameOverWindow("You lost.");

    private void ShowGameOverWindow(string message)
    {
        Window window = new Window()
        {
            Title = message
        };

        TextButton restartButton = new TextButton()
        {
            Text = "Start new game"
        };
        restartButton.TouchUp += (s, a) =>
            game.screenManager.LoadScreen(new MainMenuScreen(game));

        window.Content = restartButton;
        window.ShowModal(game.desktop);
    }
}