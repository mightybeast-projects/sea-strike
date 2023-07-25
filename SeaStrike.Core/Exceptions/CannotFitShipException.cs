using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Exceptions;

public class CannotFitShipException : SeaStrikeCoreException
{
    public CannotFitShipException(Tile startingTile) :
        base(
            "Cannot fit ship in specified starting position: " +
            startingTile.notation +
            "."
        )
    { }
}