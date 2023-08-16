using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets;

public class GameButton : TextButton
{
    private readonly SeaStrike game;

    public GameButton(SeaStrike game) : base()
    {
        this.game = game;

        Width = 130;
        HorizontalAlignment = HorizontalAlignment.Center;
        Background = new SolidBrush(Color.Black);
        Font = game.fontSystem.GetFont(24);
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);
        PressedBackground = new SolidBrush(Color.DarkGreen);
    }
}