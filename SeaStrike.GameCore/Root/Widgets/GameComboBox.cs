using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;

namespace SeaStrike.GameCore.Root.Widgets;

public class GameComboBox : ComboBox
{
    public GameComboBox(string[] items) : base()
    {
        foreach (string str in items)
            Items.Add(new ListItem(str));

        SelectedIndex = 0;

        ApplyComboBoxStyle(DefaultComboBoxStyle);

        ListBox.ApplyListBoxStyle(DefaultListBoxStyle);

        TouchUp += (s, a) => SeaStrikeGame.audioManager.buttonClickSFX.Play();
        SelectedIndexChanged += (s, a) =>
            SeaStrikeGame.audioManager.comboBoxSFX.Play();
    }

    private ComboBoxStyle DefaultComboBoxStyle =>
        new ComboBoxStyle(Stylesheet.Current.ComboBoxStyle)
        {
            LabelStyle = DefaultLabelStyle,
            Background = new SolidBrush(Color.Black),
            OverBackground = new SolidBrush(Color.Gray),
            Border = new SolidBrush(Color.LawnGreen),
            BorderThickness = new Thickness(1)
        };

    private ListBoxStyle DefaultListBoxStyle =>
        new ListBoxStyle(Stylesheet.Current.ListBoxStyle)
        {
            ListItemStyle = DefaultListItemStyle,
            Background = new SolidBrush(Color.Black),
            Border = new SolidBrush(Color.LawnGreen),
            BorderThickness = new Thickness(1)
        };

    private ImageTextButtonStyle DefaultListItemStyle =>
        new ImageTextButtonStyle()
        {
            LabelStyle = DefaultLabelStyle,
            PressedBackground = new SolidBrush(Color.DarkGreen)
        };

    private LabelStyle DefaultLabelStyle =>
        new LabelStyle(Stylesheet.Current.LabelStyle)
        {
            Font = SeaStrikeGame.fontSystem.GetFont(24),
            Padding = new Thickness(5, 0)
        };
}