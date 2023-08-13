using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;
using SeaStrike.PC.Root.UI;
using Grid = Myra.Graphics2D.UI.Grid;
using System;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D;

namespace SeaStrike.PC.Root.Screens;

public class SeaStrikeGameScreen : GameScreen
{
    private readonly SeaStrike game;
    private BoardBuilder boardBuilder;
    private Grid mainGrid;
    private GridPanel oceanGridPanel;

    public SeaStrikeGameScreen(SeaStrike game) : base(game) => this.game = game;

    public override void LoadContent()
    {
        base.LoadContent();

        boardBuilder = new BoardBuilder();

        mainGrid = new Grid()
        {
            ShowGridLines = true,
            GridLinesColor = Color.Magenta
        };

        mainGrid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        mainGrid.Widgets.Add(new Label()
        {
            Text = "Deployment phase",
            TextColor = Color.LawnGreen,
            Font = game.fontSystem.GetFont(40),
            HorizontalAlignment = HorizontalAlignment.Center
        });

        oceanGridPanel = new GridPanel(
            game,
            boardBuilder.Build().oceanGrid,
            InitializeShipAdditionDialog)
        {
            GridRow = 1,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        mainGrid.Widgets.Add(oceanGridPanel);

        game.desktop.Root = mainGrid;
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        try { game.desktop.Render(); }
        catch (Exception e) { ShowErrorDialog(e); }
    }

    public override void Update(GameTime gameTime) { }

    private void InitializeShipAdditionDialog(object sender)
    {
        if (ShipAdditionDialog.shipPool.Count == 0)
            return;

        ShipAdditionDialog dialog =
            new ShipAdditionDialog((TextButton)sender, boardBuilder);
        dialog.ShowModal(game.desktop);
        dialog.ButtonOk.TouchDown += (s, a) => oceanGridPanel.Update();
    }

    private void ShowErrorDialog(Exception e)
    {
        Dialog errorDialog = Dialog.CreateMessageBox("Error", e.Message);
        errorDialog.Background = new SolidBrush(Color.Black);
        errorDialog.Border = new SolidBrush(Color.Red);
        errorDialog.BorderThickness = new Thickness(1);

        errorDialog.ShowModal(game.desktop);
    }
}