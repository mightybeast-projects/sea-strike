using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Widgets;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : GameScreen
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGame seaStrikeGame;
    private readonly Board playerBoard;
    private Grid mainGrid;
    private Label hitResultLabel;

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
        AddPlayerOceanGridPanel();
        AddOpponentOceanGridPanel();

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
            Font = SeaStrike.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddPlayerOceanGridPanel() =>
        mainGrid.Widgets.Add(new BattleGridPanel()
            .SetGridLabel("Your's ocean grid : ")
            .SetPlayerBoard(playerBoard)
            .SetShowShips(true)
            .Build());

    private void AddOpponentOceanGridPanel()
    {
        BattleGridPanel battleGridPanel = new BattleGridPanel()
            .SetGridLabel("Opponent's ocean grid : ")
            .SetPlayerBoard(playerBoard.opponentBoard)
            .SetShowShips(false)
            .SetOnEmptyTileClicked(ShootTile)
            .Build();

        battleGridPanel.GridColumn = 1;

        hitResultLabel = new Label()
        {
            Font = SeaStrike.fontSystem.GetFont(28),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        battleGridPanel.Widgets.Add(hitResultLabel);

        mainGrid.Widgets.Add(battleGridPanel);
    }

    private void ShootTile(object sender)
    {
        string tileStr = ((TextButton)sender).Text;

        ShootResult result = seaStrikeGame.HandleShot(tileStr);

        if (seaStrikeGame.isOver)
        {
            ShowVictoryScreen();
            return;
        }

        hitResultLabel.Text = result.ToString();

        seaStrikeGame.HandleShot(tileStr);

        if (seaStrikeGame.isOver)
            ShowLostScreen();
    }

    private void ShowVictoryScreen() => new GameOverWindow(game)
    {
        Title = "You won!",
        TitleTextColor = Color.LawnGreen
    };

    private void ShowLostScreen() => new GameOverWindow(game)
    {
        Title = "You lost!",
        TitleTextColor = Color.Red
    };
}