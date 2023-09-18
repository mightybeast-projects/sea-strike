using System;
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

    public void RedirectTo<T>() where T : SeaStrikeScreen =>
        seaStrikeGame.screenManager.LoadScreen(
            (T)Activator.CreateInstance(typeof(T), this));
}