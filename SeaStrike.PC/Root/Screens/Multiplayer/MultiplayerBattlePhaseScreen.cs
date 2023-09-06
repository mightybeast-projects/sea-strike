using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using SeaStrike.PC.Root.Widgets.BattleGrid.Multiplayer;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens.Multiplayer;

public class MultiplayerBattlePhaseScreen : BattlePhaseScreen
{
    private NetPlayer player;

    public MultiplayerBattlePhaseScreen(NetPlayer player, Board opponentBoard)
        : base(player.game)
    {
        this.player = player;
        this.game = player.game;
        this.playerBoard = player.board;

        if (player.isHost)
            seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard);
        else
        {
            seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard, true);
            player.canShoot = false;
        }

        player.seaStrikeGame = seaStrikeGame;
    }

    public override void LoadContent()
    {
        Grid mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(PhaseLabel);
        mainGrid.Widgets.Add(HelpButton);
        mainGrid.Widgets.Add(PlayerBattleGridPanel);
        mainGrid.Widgets.Add(CurrentPlayerTurnLabel);
        mainGrid.Widgets.Add(OpponentBattleGridPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetwork();

        UpdateCurrentTurnLabel();
    }

    protected override OpponentBattleGridPanel OpponentBattleGridPanel =>
        new MultiplayerOpponnentBattleGridPanel(
            player, seaStrikeGame, playerBoard.opponentBoard)
        {
            GridRow = 1,
            GridColumn = 1
        };

    private Label CurrentPlayerTurnLabel => new Label()
    {
        Top = -20,
        GridRow = 1,
        Font = SeaStrike.fontSystem.GetFont(28),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Bottom
    };

    private void UpdateCurrentTurnLabel()
    {
        Label label = (Label)((Grid)game.desktop.Root).Widgets[3];

        if (player.canShoot)
        {
            label.Text = SeaStrike.stringStorage.yourTurnLabel;
            label.TextColor = Color.LawnGreen;
        }
        else
        {
            label.Text = SeaStrike.stringStorage.opponentsTurnLabel;
            label.TextColor = Color.Red;
        }
    }
}