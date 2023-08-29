using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public abstract class BattleGridPanel : VerticalStackPanel
{
    internal string gridLabel;
    internal Board playerBoard;
    internal bool showShips;
    internal Action<object> OnEmptyTileClicked;

    public void Initialize()
    {
        GridRow = 1;
        Spacing = 10;

        Widgets.Add(new Label()
        {
            Text = gridLabel,
            Font = SeaStrike.fontSystem.GetFont(28),
            TextColor = Color.LawnGreen,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        GridPanel oceanGridPanel =
            new GridPanel(playerBoard.oceanGrid, showShips)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                OnEmptyTileClicked = OnEmptyTileClicked
            };
        playerBoard.Subscribe(oceanGridPanel);

        Widgets.Add(oceanGridPanel);
    }
}