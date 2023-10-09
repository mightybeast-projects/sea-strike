using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.GameCore.Root.Screens;
using SeaStrike.GameCore.Root.Widgets.Modal;
using Color = Microsoft.Xna.Framework.Color;

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

    public virtual void ShowVictoryScreen() =>
        new GameOverWindow(
            SeaStrikeGame.stringStorage.victoryScreenTitle,
            onGameOverScreenExitButtonClicked)
        {
            TitleTextColor = Color.LawnGreen
        }.ShowModal(seaStrikeGame.desktop);

    public virtual void ShowLostScreen() =>
        new GameOverWindow(
            SeaStrikeGame.stringStorage.loseScreenTitle,
            onGameOverScreenExitButtonClicked)
        {
            TitleTextColor = Color.Red
        }.ShowModal(seaStrikeGame.desktop);
}