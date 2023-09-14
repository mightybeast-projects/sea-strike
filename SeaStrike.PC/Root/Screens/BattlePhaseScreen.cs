using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.Modal;

using Grid = Myra.Graphics2D.UI.Grid;
using Game = SeaStrike.Core.Entity.GameLogic.Game;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : SeaStrikeScreen
{
    protected Game game;
    protected Board playerBoard;

    public BattlePhaseScreen(SeaStrikeGame seaStrikeGame, Board playerBoard)
        : base(seaStrikeGame)
    {
        this.playerBoard = playerBoard;

        this.game = new Game(playerBoard);
    }

    protected BattlePhaseScreen(SeaStrikeGame seaStrikeGame)
        : base(seaStrikeGame) { }

    public override void LoadContent()
    {
        base.LoadContent();

        Grid mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(PlayerBattleGridPanel);
        mainGrid.Widgets.Add(OpponentBattleGridPanel);

        base.seaStrikeGame.desktop.Root = mainGrid;
    }

    protected Label PhaseLabel => new Label()
    {
        Text = SeaStrikeGame.stringStorage.battlePhaseScreenTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrikeGame.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center,
        GridColumnSpan = 2
    };

    protected GameButton HelpButton =>
        new GameButton(() =>
            ShowHelpWindow(SeaStrikeGame.stringStorage.bpHelpWindowContent))
        {
            Width = 40,
            Height = 40,
            Text = SeaStrikeGame.stringStorage.helpButtonLabel,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
        };

    protected PlayerBattleGridPanel PlayerBattleGridPanel =>
        new PlayerBattleGridPanel(playerBoard)
        {
            GridRow = 1
        };

    protected virtual OpponentBattleGridPanel OpponentBattleGridPanel =>
        new OpponentBattleGridPanel(
            base.seaStrikeGame, game, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };

    private void ShowHelpWindow(string[] content) =>
        new HelpWindow(content).ShowModal(base.seaStrikeGame.desktop);
}