using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrikePlayer
{
    public readonly SeaStrikeGame seaStrikeGame;
    public Game game;
    public Board board;

    public SeaStrikePlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void RedirectToMainMenu() =>
        seaStrikeGame.screenManager.LoadScreen(
            new MainMenuScreen(seaStrikeGame));
}