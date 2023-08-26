namespace SeaStrike.Core.Entity.Game;

public class AIPlayer : Player
{
    public AIPlayer() : base(
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build())
    { }

    public void Shoot() { }
}