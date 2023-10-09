using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using FontStashSharp.RichText;

namespace SeaStrike.GameCore.Root.Widgets.Modal;

public class DisconnectionWarningWindow : GameWindow
{
    public DisconnectionWarningWindow(Action onConfirmButtonClicked)
        : base(SeaStrikeGame.stringStorage.disconnectionWarningWindowTitle)
    {
        TitleTextColor = Color.Red;
        Border = new SolidBrush(Color.Red);
        DragHandle = null;

        VerticalStackPanel content = new VerticalStackPanel()
        {
            Spacing = 10
        };

        content.Widgets.Add(WarningLabel);
        content.Widgets.Add(ConfirmButton(onConfirmButtonClicked));

        Content = content;
    }

    private Label WarningLabel = new Label()
    {
        Text = SeaStrikeGame.stringStorage.disconnectionWarningLabel,
        TextAlign = TextHorizontalAlignment.Center
    };

    private GameButton ConfirmButton(Action onConfirmButtonClicked) =>
        new GameButton(onConfirmButtonClicked)
        {
            Text = SeaStrikeGame.stringStorage.confirmButtonLabel
        };
}