using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.Core.Entity.Game.Utility;
using SeaStrike.PC.Root.Widgets;
using SeaStrike.PC.Root.Widgets.BattleGrid;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : GameScreen
{
    private readonly SeaStrike game;
    private readonly SeaStrikeGame seaStrikeGame;
    private readonly Board playerBoard;
    private Grid mainGrid;

    public BattlePhaseScreen(SeaStrike game, Board playerBoard)
        : base(game)
    {
        this.game = game;
        this.playerBoard = playerBoard;

        seaStrikeGame = new SeaStrikeGame(playerBoard);
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        AddPhaseLabel();
        AddHelpButton();
        AddPlayerBattleGridPanel();
        AddOpponentOceanGridPanel();

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }

    private void AddPhaseLabel()
    {
        mainGrid.Widgets.Add(new Label()
        {
            Text = SeaStrike.stringStorage.battlePhaseScreenTitle,
            TextColor = Color.LawnGreen,
            Font = SeaStrike.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddHelpButton()
    {
        mainGrid.Widgets.Add(new HelpButton(game)
        {
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            helpWindowContent = SeaStrike.stringStorage.bpHelpWindowContent
        });
    }

    private void AddPlayerBattleGridPanel() =>
        mainGrid.Widgets.Add(new PlayerBattleGridPanel(playerBoard));

    private void AddOpponentOceanGridPanel()
    {
        mainGrid.Widgets.Add(new OpponentBattleGridPanel(
            game,
            seaStrikeGame,
            playerBoard.opponentBoard
        )
        {
            GridColumn = 1
        });
    }
}