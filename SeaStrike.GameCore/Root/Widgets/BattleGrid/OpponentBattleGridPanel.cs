using System.Linq;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic.Utility;
using SeaStrike.GameCore.Root.Widgets.GridTile;

using Game = SeaStrike.Core.Entity.GameLogic.Game;

namespace SeaStrike.GameCore.Root.Widgets.BattleGrid;

public class OpponentBattleGridPanel : BattleGridPanel
{
    protected override Board playerBoard => player.board.opponentBoard;
    protected Game game => player.game;
    protected Label shotResultLabel => (Label)Widgets.Last();

    private SeaStrikeGame seaStrikeGame => player.seaStrikeGame;

    public OpponentBattleGridPanel(SeaStrikePlayer player) : base(player)
    {
        gridLabel = SeaStrikeGame.stringStorage.opponentOceanGridLabel;
        OnEmptyTileClicked += ShootTile;
        showShips = false;

        Initialize();

        Widgets.Add(HitResultLabel);
    }

    protected virtual void ShootTile(object sender)
    {
        string tileStr = ((EmptyGridTileButton)sender).tile.notation;

        ShotResult result = game.HandleCurrentPlayerShot(tileStr);

        shotResultLabel.Text = result.ToString();

        SeaStrikeGame.audioManager.PlayShotResultSFX(result);

        if (game.isOver)
            player.ShowVictoryScreen();
        else
            MakeAIPlayerShoot();
    }

    private Label HitResultLabel => new Label()
    {
        Font = SeaStrikeGame.fontSystem.GetFont(28),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center,
    };

    private void MakeAIPlayerShoot()
    {
        game.HandleAIPlayerShot();

        if (game.isOver)
            player.ShowLostScreen();
    }
}