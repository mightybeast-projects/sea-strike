using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.Widgets;
using Grid = Myra.Graphics2D.UI.Grid;

namespace SeaStrike.PC.Root.Screens;

public class BattlePhaseScreen : GameScreen
{
    private readonly SeaStrike game;
    private readonly BoardBuilder boardBuilder;
    private Grid mainGrid;

    public BattlePhaseScreen(SeaStrike game, BoardBuilder boardBuilder)
        : base(game)
    {
        this.game = game;
        this.boardBuilder = boardBuilder;
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

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime) { }

    public override void Update(GameTime gameTime) { }
}