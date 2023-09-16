using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrikePlayer
{
    public readonly SeaStrikeGame seaStrikeGame;
    public Game game { get; protected set; }
    public BoardBuilder boardBuilder;

    public Board board => boardBuilder.Build();

    public SeaStrikePlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public virtual void StartCoreGame() => game = new Game(board);

    public void RedirectToMainMenuScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new MainMenuScreen(this));

    public virtual void RedirectToDeploymentScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new DeploymentPhaseScreen(this));

    public virtual void RedirectToBattleScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new BattlePhaseScreen(this));

    public void RedirectToLobbyScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new LobbyScreen(this));
}