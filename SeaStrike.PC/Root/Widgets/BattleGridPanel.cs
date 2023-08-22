using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets;

public class BattleGridPanel : VerticalStackPanel
{
    private string gridLabel;
    private Board playerBoard;
    private bool showShips;
    private Action<object> OnEmptyTileClicked;

    public BattleGridPanel SetGridLabel(string gridLabel)
    {
        this.gridLabel = gridLabel;

        return this;
    }

    public BattleGridPanel SetPlayerBoard(Board playerBoard)
    {
        this.playerBoard = playerBoard;

        return this;
    }

    public BattleGridPanel SetShowShips(bool showShips)
    {
        this.showShips = showShips;

        return this;
    }

    public BattleGridPanel SetOnEmptyTileClicked(Action<object> action)
    {
        this.OnEmptyTileClicked = action;

        return this;
    }

    public BattleGridPanel Build()
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

        return this;
    }
}