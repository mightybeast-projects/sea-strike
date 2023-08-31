using System;
using SeaStrike.PC.Root.Widgets.Modal;

namespace SeaStrike.PC.Root.Widgets.Button;

public class HelpButton : GameButton
{
    public string[] helpWindowContent;

    public HelpButton(SeaStrike game) : base()
    {
        Text = SeaStrike.stringStorage.helpButtonLabel;
        Width = 40;
        Height = 40;

        TouchUp += (s, a) =>
            new HelpWindow(helpWindowContent).ShowModal(game.desktop);
    }
}