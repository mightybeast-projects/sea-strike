using SeaStrike.Core.Entity;

namespace SeaStrike.Core.Exceptions;

public class TileIsOccupiedByOtherShipException : SeaStrikeCoreException
{
    public TileIsOccupiedByOtherShipException(Tile occupiedTile) :
        base(occupiedTile.notation + " is occupied by another ship.")
    { }
}