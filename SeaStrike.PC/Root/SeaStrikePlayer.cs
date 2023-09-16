using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrikePlayer
{
    public readonly SeaStrikeGame seaStrikeGame;
    public BoardBuilder boardBuilder;
    public Game game;

    public Board board => boardBuilder.Build();

    public SeaStrikePlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void RedirectToMainMenuScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new MainMenuScreen(this));

    public void RedirectToDeploymentPhaseScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new DeploymentPhaseScreen(this));

    public void RedirectToBattleScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new BattlePhaseScreen(this));

    public void RedirectToLobbyScreen() =>
        seaStrikeGame.screenManager.LoadScreen(new LobbyScreen(this));
}