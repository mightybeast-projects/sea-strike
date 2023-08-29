using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public class OpponentBattleGridPanel : BattleGridPanel
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGame seaStrikeGame;
    private readonly Label hitResultLabel;

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

        hitResultLabel = new Label()
        {
            Font = SeaStrike.fontSystem.GetFont(28),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        Widgets.Add(hitResultLabel);
    }

    private void ShootTile(object sender)
    {
        string tileStr = ((TextButton)sender).Text;

        ShotResult result = seaStrikeGame.HandleCurrentPlayerShot(tileStr);

        hitResultLabel.Text = result.ToString();

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

    private void ShowVictoryScreen() => new GameOverWindow(game)
    {
        Title = SeaStrike.stringStorage.victoryScreenTitle,
        TitleTextColor = Color.LawnGreen
    }.ShowModal(game.desktop);

    private void ShowLostScreen() => new GameOverWindow(game)
    {
        Title = SeaStrike.stringStorage.loseScreenTitle,
        TitleTextColor = Color.Red
    }.ShowModal(game.desktop);
}