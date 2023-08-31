using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public abstract class BattleGridPanel : VerticalStackPanel
{
    protected string gridLabel;
    protected Board playerBoard;
    protected bool showShips;
    protected Action<object> OnEmptyTileClicked;

    public void Initialize()
    {
        Spacing = 10;

        Widgets.Add(GridLabel);
        Widgets.Add(PlayerOceanGrid);
    }

    private Label GridLabel => new Label()
    {
        Text = gridLabel,
        Font = SeaStrike.fontSystem.GetFont(28),
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