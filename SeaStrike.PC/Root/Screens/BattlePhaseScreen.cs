using System.Text;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;
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

        seaStrikeGame = new SeaStrikeGame(playerBoard);
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        AddPhaseLabel();
        AddHelpButton();
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
            Text = SeaStrike.stringStorage.battlePhaseScreenTitle,
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddHelpButton()
    {
        TextButton helpButton = new GameButton()
        {
            Text = SeaStrike.stringStorage.helpButtonLabel,
            Width = 40,
            Height = 40,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top
        };

        string[] helpWindowContent = SeaStrike.stringStorage.helpWindowContent;
        StringBuilder builder = new StringBuilder();
        foreach (string str in helpWindowContent)
            builder.Append(str);

        string helpContent = builder.ToString();
        helpButton.TouchUp += (s, a) =>
            new HelpWindow(helpContent).ShowModal(game.desktop);

        mainGrid.Widgets.Add(helpButton);
    }

    private void AddPlayerOceanGridPanel() =>
        mainGrid.Widgets.Add(new BattleGridPanel()
            .SetGridLabel(SeaStrike.stringStorage.playerOceanGridLabel)
            .SetPlayerBoard(playerBoard)
            .SetShowShips(true)
            .Build());

    private void AddOpponentOceanGridPanel()
    {
        BattleGridPanel battleGridPanel = new BattleGridPanel()
            .SetGridLabel(SeaStrike.stringStorage.opponentOceanGridLabel)
            .SetPlayerBoard(playerBoard.opponentBoard)
            .SetShowShips(false)
            .AddOnEmptyTileClickedAction(ShootTile)
            .AddOnEmptyTileClickedAction(MakeAIPlayerShoot)
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

        ShotResult result = seaStrikeGame.HandleCurrentPlayerShot(tileStr);

        hitResultLabel.Text = result.ToString();

        if (seaStrikeGame.isOver)
            ShowVictoryScreen();
    }

    private void MakeAIPlayerShoot(object sender)
    {
        if (seaStrikeGame.isOver)
            return;

        seaStrikeGame.HandleAIPlayerShot();

        if (seaStrikeGame.isOver)
            ShowLostScreen();
    }

    private void ShowVictoryScreen() => new GameOverWindow(game)
    {
        Title = SeaStrike.stringStorage.victoryScreenTitle,
        TitleTextColor = Color.LawnGreen
    };

    private void ShowLostScreen() => new GameOverWindow(game)
    {
        Title = SeaStrike.stringStorage.loseScreenTitle,
        TitleTextColor = Color.Red
    };
}