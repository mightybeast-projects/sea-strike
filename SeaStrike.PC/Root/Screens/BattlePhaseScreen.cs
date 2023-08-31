using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.Modal;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : GameScreen
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGame seaStrikeGame;
    private readonly Board playerBoard;

    public BattlePhaseScreen(SeaStrike game, Board playerBoard) : base(game)
    {
        this.game = game;
        this.playerBoard = playerBoard;

        seaStrikeGame = new SeaStrikeGame(playerBoard);
    }

    public override void LoadContent()
    {
        base.LoadContent();

        Grid mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(PlayerBattleGridPanel);
        mainGrid.Widgets.Add(OpponentBattleGridPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }

    private Label PhaseLabel => new Label()
    {
        Text = SeaStrike.stringStorage.battlePhaseScreenTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrike.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center,
        GridColumnSpan = 2
    };

    private GameButton HelpButton =>
        new GameButton(() =>
            ShowHelpWindow(SeaStrike.stringStorage.dpHelpWindowContent))
        {
            Width = 40,
            Height = 40,
            Text = SeaStrike.stringStorage.helpButtonLabel,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
        };

    private PlayerBattleGridPanel PlayerBattleGridPanel =>
        new PlayerBattleGridPanel(playerBoard)
        {
            GridRow = 1
        };

    private OpponentBattleGridPanel OpponentBattleGridPanel =>
        new OpponentBattleGridPanel(
            game, seaStrikeGame, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };

    private void ShowHelpWindow(string[] content) =>
        new HelpWindow(content).ShowModal(game.desktop);
}