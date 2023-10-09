using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.Widgets.Modal;

public class GameWindow : Window
{
    public GameWindow(string title)
    {
        Title = title;
        TitleFont = SeaStrikeGame.fontSystem.GetFont(30);
        TitleTextColor = Color.LawnGreen;

        TitleGrid.Background = new SolidBrush(Color.Black);
        TitleGrid.Border = new SolidBrush(Color.DimGray);
        TitleGrid.BorderThickness = new Thickness(0, 0, 0, 1);

        Background = new SolidBrush(Color.Black);
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(1);

        CloseButton.Border = new SolidBrush(Color.Red);
        CloseButton.BorderThickness = new Thickness(2);
    }
}