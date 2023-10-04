using System;

namespace SeaStrike.PC.Root.Widgets.Modal;

public class GameOverWindow : GameWindow
{
    private readonly Action onExitButtonClicked;

    public GameOverWindow(string title, Action onExitButtonClicked)
        : base(title)
    {
        this.onExitButtonClicked = onExitButtonClicked;

        DragHandle = null;
        CloseButton.Visible = false;

        Content = ExitButton;
    }

    private GameButton ExitButton => new GameButton(onExitButtonClicked)
    {
        Text = SeaStrikeGame.stringStorage.exitButtonLabel
    };
}