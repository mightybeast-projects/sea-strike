namespace SeaStrike.Core.Entity;

public class AIPlayer : Player
{
    public AIPlayer() : base(
        new BoardBuilder()
        .RandomizeShipsStartingPosition()
        .Build())
    { }

    public void Shoot() { }
}