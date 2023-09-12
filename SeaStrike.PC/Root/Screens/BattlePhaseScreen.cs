using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.Modal;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : SeaStrikeScreen
{
    protected SeaStrikeGame seaStrikeGame;
    protected Board playerBoard;

    public BattlePhaseScreen(SeaStrike game, Board playerBoard) : base(game)
    {
        this.playerBoard = playerBoard;

        seaStrikeGame = new SeaStrikeGame(playerBoard);
    }

    protected BattlePhaseScreen(SeaStrike game) : base(game) { }

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

    protected Label PhaseLabel => new Label()
    {
        Text = SeaStrike.stringStorage.battlePhaseScreenTitle,
        TextColor = Color.LawnGreen,
        Font = SeaStrike.fontSystem.GetFont(40),
        HorizontalAlignment = HorizontalAlignment.Center,
        GridColumnSpan = 2
    };

    protected GameButton HelpButton =>
        new GameButton(() =>
            ShowHelpWindow(SeaStrike.stringStorage.bpHelpWindowContent))
        {
            Width = 40,
            Height = 40,
            Text = SeaStrike.stringStorage.helpButtonLabel,
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
            game, seaStrikeGame, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };

    private void ShowHelpWindow(string[] content) =>
        new HelpWindow(content).ShowModal(game.desktop);
}