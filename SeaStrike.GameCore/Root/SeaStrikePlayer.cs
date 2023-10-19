using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.GameCore.Root.Screens;
using SeaStrike.GameCore.Root.Widgets.Modal;

namespace SeaStrike.GameCore.Root;

public class SeaStrikePlayer
{
    public readonly SeaStrikeGame seaStrikeGame;
    public Game game { get; protected set; }
    public BoardBuilder boardBuilder;

    public Board board => boardBuilder.Build();

    protected virtual Action onGameOverScreenExitButtonClicked =>
        RedirectTo<MainMenuScreen>;

    public SeaStrikePlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void StartCoreGame() => game = new Game(board);

    public void RedirectTo<T>() where T : SeaStrikeScreen =>
        seaStrikeGame.screenManager.LoadScreen(
            (T)Activator.CreateInstance(typeof(T), this));

    public void ShowVictoryScreen() =>
        new VictoryWindow(onGameOverScreenExitButtonClicked)
        .ShowModal(seaStrikeGame.desktop);

    public void ShowLostScreen() =>
        new LostWindow(onGameOverScreenExitButtonClicked)
        .ShowModal(seaStrikeGame.desktop);
}