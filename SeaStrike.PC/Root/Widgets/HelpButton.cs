namespace SeaStrike.PC.Root.Widgets;

public class HelpButton : GameButton
{
    public string[] helpWindowContent;

    public HelpButton(SeaStrike game)
    {
        Text = SeaStrike.stringStorage.helpButtonLabel;
        Width = 40;
        Height = 40;

        TouchUp += (s, a) =>
            new HelpWindow(helpWindowContent).ShowModal(game.desktop);
    }
}