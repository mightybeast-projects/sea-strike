using System.Linq;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;
using SeaStrike.PC.Root.Widgets.GridTile;
using SeaStrike.PC.Root.Widgets.Modal;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public class OpponentBattleGridPanel : BattleGridPanel
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGame seaStrikeGame;

    public OpponentBattleGridPanel(
        SeaStrike game,
        SeaStrikeGame seaStrikeGame,
        Board opponentBoard)
    {
        this.game = game;
        this.seaStrikeGame = seaStrikeGame;

        playerBoard = opponentBoard;
        gridLabel = SeaStrike.stringStorage.opponentOceanGridLabel;
        OnEmptyTileClicked += ShootTile;
        OnEmptyTileClicked += MakeAIPlayerShoot;
        showShips = false;

        Initialize();

        Widgets.Add(HitResultLabel);
    }

    private Label HitResultLabel => new Label()
    {
        Font = SeaStrike.fontSystem.GetFont(28),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center,
    };

    private void ShootTile(object sender)
    {
        string tileStr = ((EmptyGridTileButton)sender).tile.notation;

        ShotResult result = seaStrikeGame.HandleCurrentPlayerShot(tileStr);

        ((Label)Widgets.Last()).Text = result.ToString();

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

    private void ShowVictoryScreen() =>
        new GameOverWindow(game, SeaStrike.stringStorage.victoryScreenTitle)
        {
            TitleTextColor = Color.LawnGreen
        }.ShowModal(game.desktop);

    private void ShowLostScreen() =>
        new GameOverWindow(game, SeaStrike.stringStorage.loseScreenTitle)
        {
            TitleTextColor = Color.Red
        }.ShowModal(game.desktop);
}