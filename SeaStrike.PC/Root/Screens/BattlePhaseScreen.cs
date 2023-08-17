using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
using Grid = Myra.Graphics2D.UI.Grid;
using SeaStrikeGame = SeaStrike.Core.Entity.Game;

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

        Board opponentBoard = new BoardBuilder()
            .RandomizeShipsStartingPosition()
            .Build();

        seaStrikeGame = new SeaStrikeGame(playerBoard, opponentBoard);
    }

    public override void LoadContent()
    {
        base.LoadContent();

        mainGrid = new Grid();

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        AddPhaseLabel();
        AddOceanGridPanel();
        AddTargetGridPanel();

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }

    private void AddPhaseLabel()
    {
        mainGrid.Widgets.Add(new Label()
        {
            Text = "Battle phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });
    }

    private void AddOceanGridPanel()
    {
        GridPanel oceanGridPanel =
            new GridPanel(game, playerBoard.oceanGrid, true)
            {
                GridRow = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        playerBoard.Subscribe(oceanGridPanel);

        mainGrid.Widgets.Add(oceanGridPanel);
    }

    private void AddTargetGridPanel()
    {
        GridPanel targetGridPanel =
            new GridPanel(game, playerBoard.targetGrid, false)
            {
                GridRow = 1,
                GridColumn = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                OnEmptyTileClicked = ShootTile
            };
        playerBoard.opponentBoard.Subscribe(targetGridPanel);

        mainGrid.Widgets.Add(targetGridPanel);
    }

    private void ShootTile(object sender)
    {
        string tileStr = ((TextButton)sender).Text;

        seaStrikeGame.HandleShot(tileStr);
        seaStrikeGame.HandleShot(tileStr);
    }
}