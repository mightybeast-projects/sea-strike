using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;

using Grid = Myra.Graphics2D.UI.Grid;
using Game = SeaStrike.Core.Entity.GameLogic.Game;

namespace SeaStrike.PC.Root.Screens.Multiplayer;

public class NetBattlePhaseScreen : BattlePhaseScreen
{
    private NetPlayer player;
    private Grid mainGrid;

    public NetBattlePhaseScreen(NetPlayer player, Board opponentBoard)
        : base(player.seaStrikeGame)
    {
        this.player = player;
        this.playerBoard = player.board;

        if (player.isHost)
            game = new Game(playerBoard, opponentBoard);
        else
            game = new Game(playerBoard, opponentBoard, true);

        player.game = game;
    }

    public override void LoadContent()
    {
        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(PlayerBattleGridPanel);
        mainGrid.Widgets.Add(CurrentPlayerTurnLabel);
        mainGrid.Widgets.Add(OpponentBattleGridPanel);

        seaStrikeGame.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetManagers();

        ((CurrentPlayerTurnLabel)mainGrid.Widgets[3]).Update();
    }

    protected override OpponentBattleGridPanel OpponentBattleGridPanel =>
        new NetOpponnentBattleGridPanel(
            player, game, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };

    private Label CurrentPlayerTurnLabel => new CurrentPlayerTurnLabel(player)
    {
        Top = -20,
        GridRow = 1,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Bottom
    };
}