using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.GameCore.Root.Network;
using SeaStrike.GameCore.Root.Widgets.BattleGrid;
using SeaStrike.GameCore.Root.Widgets.BattleGrid.Multiplayer;

using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.GameCore.Root.Screens.Multiplayer;

public class NetBattlePhaseScreen : BattlePhaseScreen
{
    private new NetPlayer player => (NetPlayer)base.player;
    private Grid mainGrid;

    public NetBattlePhaseScreen(NetPlayer player) : base(player) { }

    public override void LoadContent()
    {
        SeaStrikeGame.audioManager.PlayBattleOST();

        player.StartCoreGame();

        mainGrid = new Grid()
        {
            ColumnSpacing = 40
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(PlayerBattleGridPanel);
        mainGrid.Widgets.Add(OpponentBattleGridPanel);

        seaStrikeGame.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetManagers();

        ((NetPlayerBattleGridPanel)mainGrid.Widgets[2]).Update();
    }

    protected override PlayerBattleGridPanel PlayerBattleGridPanel =>
        new NetPlayerBattleGridPanel(player)
        {
            GridRow = 1,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

    protected override OpponentBattleGridPanel OpponentBattleGridPanel =>
        new NetOpponnentBattleGridPanel(player)
        {
            GridRow = 1,
            GridColumn = 1,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
}