using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrikePlayer
{
    internal readonly SeaStrikeGame seaStrikeGame;
    internal Game game;
    internal Board board;

    internal bool canShoot => game.currentPlayer.board == board;

    public SeaStrikePlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void RedirectToMainMenu() =>
        seaStrikeGame.screenManager.LoadScreen(
            new MainMenuScreen(seaStrikeGame));
}