namespace SeaStrike.Core.Exceptions;

public class CannotFindSpecifiedTileException : SeaStrikeCoreException
{
    public CannotFindSpecifiedTileException(string tileNotation) :
        base("Cannot find specified tile: " + tileNotation + ".")
    { }
}