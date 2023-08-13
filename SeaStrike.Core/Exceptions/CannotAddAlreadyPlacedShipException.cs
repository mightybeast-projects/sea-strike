using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Exceptions;

public class CannotAddAlreadyPlacedShipException : Exception
{
    public CannotAddAlreadyPlacedShipException(Ship ship) :
        base("You have already placed a ship of type " + ship.GetType().Name + ".")
    { }
}