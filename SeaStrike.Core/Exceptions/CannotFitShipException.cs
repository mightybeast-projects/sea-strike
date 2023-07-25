namespace SeaStrike.Core.Exceptions;

public class CannotFitShipException : SeaStrikeCoreException
{
    public CannotFitShipException(string startingPosition) :
        base(
            "Cannot fit ship in specified starting position: " +
            startingPosition +
            "."
        )
    { }
}