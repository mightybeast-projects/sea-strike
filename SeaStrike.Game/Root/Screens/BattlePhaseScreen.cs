using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.Modal;

using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : SeaStrikeScreen
{
    public BattlePhaseScreen(SeaStrikePlayer player) : base(player) { }

    public override void LoadContent()
    {
        player.StartCoreGame();

        Grid mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(PlayerBattleGridPanel);
        mainGrid.Widgets.Add(OpponentBattleGridPanel);

        seaStrikeGame.desktop.Root = mainGrid;
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

    protected virtual PlayerBattleGridPanel PlayerBattleGridPanel =>
        new PlayerBattleGridPanel(player)
        {
            Top = -19,
            GridRow = 1,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

    protected virtual OpponentBattleGridPanel OpponentBattleGridPanel =>
        new OpponentBattleGridPanel(player)
        {
            Width = 450,
            GridRow = 1,
            GridColumn = 1,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

    private void ShowHelpWindow(string[] content) =>
        new HelpWindow(content).ShowModal(base.seaStrikeGame.desktop);
}