using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.GameCore.Root.Widgets.BattleGrid;

public abstract class BattleGridPanel : VerticalStackPanel
{
    protected SeaStrikePlayer player;
    protected string gridLabel;
    protected bool showShips;
    protected Action<object> OnEmptyTileClicked;

    protected abstract Board playerBoard { get; }

    protected BattleGridPanel(SeaStrikePlayer player) => this.player = player;

    protected void Initialize()
    {
        Spacing = 10;

        Widgets.Add(GridLabel);
        Widgets.Add(PlayerOceanGrid);
    }

    private Label GridLabel => new Label()
    {
        Text = gridLabel,
        Font = SeaStrikeGame.fontSystem.GetFont(28),
        TextColor = Color.LawnGreen,
        HorizontalAlignment = HorizontalAlignment.Center
    };

    private GridPanel PlayerOceanGrid => new GridPanel(playerBoard, showShips)
    {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        OnEmptyTileClicked = OnEmptyTileClicked
    };
}