using System;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Widgets.Modal;

using Color = Microsoft.Xna.Framework.Color;

namespace SeaStrike.PC.Root;

public class SeaStrikePlayer
{
    public readonly SeaStrikeGame seaStrikeGame;
    public Game game { get; protected set; }
    public BoardBuilder boardBuilder;

    public Board board => boardBuilder.Build();

    public SeaStrikePlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void StartCoreGame() => game = new Game(board);

    public void RedirectTo<T>() where T : SeaStrikeScreen =>
        seaStrikeGame.screenManager.LoadScreen(
            (T)Activator.CreateInstance(typeof(T), this));

    public void ShowVictoryScreen() =>
        new GameOverWindow(this, SeaStrikeGame.stringStorage.victoryScreenTitle)
        {
            TitleTextColor = Color.LawnGreen
        }.ShowModal(seaStrikeGame.desktop);

    public void ShowLostScreen() =>
        new GameOverWindow(this, SeaStrikeGame.stringStorage.loseScreenTitle)
        {
            TitleTextColor = Color.Red
        }.ShowModal(seaStrikeGame.desktop);
}