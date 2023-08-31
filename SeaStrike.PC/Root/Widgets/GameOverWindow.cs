namespace SeaStrike.PC.Root.Widgets;

public class GameOverWindow : GameWindow
{
    public GameOverWindow(SeaStrike game)
    {
        DragHandle = null;
        CloseButton.Visible = false;

        Content = new RestartButton(game);
    }
}