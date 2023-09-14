using System.Linq;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic.Utility;
using SeaStrike.PC.Root.Widgets.GridTile;

using Game = SeaStrike.Core.Entity.GameLogic.Game;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public class OpponentBattleGridPanel : BattleGridPanel
{
    protected readonly Game game;
    protected Label shotResultLabel => (Label)Widgets.Last();

    private readonly SeaStrikeGame seaStrikeGame;

    public OpponentBattleGridPanel(
        SeaStrikeGame seaStrikeGame,
        Game game,
        Board opponentBoard)
    {
        this.seaStrikeGame = seaStrikeGame;
        this.game = game;

        playerBoard = opponentBoard;
        gridLabel = SeaStrikeGame.stringStorage.opponentOceanGridLabel;
        OnEmptyTileClicked += ShootTile;
        showShips = false;

        Initialize();

        Widgets.Add(HitResultLabel);
    }

    private Label HitResultLabel => new Label()
    {
        Font = SeaStrikeGame.fontSystem.GetFont(28),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center,
    };

    protected virtual void ShootTile(object sender)
    {
        string tileStr = ((EmptyGridTileButton)sender).tile.notation;

        ShotResult result = game.HandleCurrentPlayerShot(tileStr);

        shotResultLabel.Text = result.ToString();

        if (game.isOver)
            seaStrikeGame.ShowVictoryScreen();

        MakeAIPlayerShoot();
    }

    private void MakeAIPlayerShoot()
    {
        if (game.isOver)
            return;

        game.HandleAIPlayerShot();

        if (game.isOver)
            seaStrikeGame.ShowLostScreen();
    }
}