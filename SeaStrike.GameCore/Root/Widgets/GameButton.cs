using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.GameCore.Root.Widgets;

public class GameButton : TextButton
{
    public GameButton(Action onClick = null)
    {
        HorizontalAlignment = HorizontalAlignment.Center;
        Background = new SolidBrush(Color.Black);
        Font = SeaStrikeGame.fontSystem.GetFont(24);
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(2);
        PressedBackground = new SolidBrush(Color.DarkGreen);
        Padding = new Thickness(5, 0);

        TouchUp += (s, a) => onClick?.Invoke();
        TouchUp += (s, a) => SeaStrikeGame.audioManager.PlayButtonClickSFX();
    }
}