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
    private readonly BoardBuilder boardBuilder;
    private readonly Board playerBoard;
    private readonly SeaStrikeGame seaStrikeGame;
    private Grid mainGrid;

    public BattlePhaseScreen(SeaStrike game, BoardBuilder boardBuilder)
        : base(game)
    {
        this.game = game;
        this.boardBuilder = boardBuilder;
        playerBoard = boardBuilder.Build();

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

        mainGrid.Widgets.Add(new Label()
        {
            Text = "Battle phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center,
            GridColumnSpan = 2
        });

        AllyGridPanel oceanGridPanel = new AllyGridPanel(game, playerBoard.oceanGrid)
        {
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            OnEmptyTileClicked = (s) => { }
        };
        playerBoard.Subscribe(oceanGridPanel);

        mainGrid.Widgets.Add(oceanGridPanel);

        EnemyGridPanel targetGridPanel = new EnemyGridPanel(game, playerBoard.targetGrid)
        {
            GridRow = 1,
            GridColumn = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            OnEmptyTileClicked = ShootTile
        };
        playerBoard.opponentBoard.Subscribe(targetGridPanel);

        mainGrid.Widgets.Add(targetGridPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }

    private void ShootTile(object sender)
    {
        string tileStr = ((TextButton)sender).Text;

        seaStrikeGame.HandleShot(tileStr);
        seaStrikeGame.HandleShot(tileStr);
    }
}