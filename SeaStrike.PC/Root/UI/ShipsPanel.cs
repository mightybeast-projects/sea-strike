using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace SeaStrike.PC.Root.UI;

public class ShipsPanel : Panel
{
    public ShipsPanel(SeaStrike game)
    {
        Width = 200;
        Height = 300;
        Border = new SolidBrush(Color.LawnGreen);
        BorderThickness = new Thickness(0, 1, 1, 1);

        Widgets.Add(new Label()
        {
            Text = "Ships :",
            Font = game.fontSystem.GetFont(36),
            HorizontalAlignment = HorizontalAlignment.Center
        });
    }
}