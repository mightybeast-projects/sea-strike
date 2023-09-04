using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Network;

public class BoardData
{
    public List<ShipData> shipDatas;

    public BoardData(Board board)
    {
        shipDatas = new List<ShipData>();

        foreach (Ship ship in board.ships)
            shipDatas.Add(NewShipData(ship));
    }

    public string ToJson() => JsonConvert.SerializeObject(this);

    private Func<Ship, ShipData> NewShipData = (Ship ship) => new ShipData()
    {
        orientation = ship.orientation.ToString(),
        shipType = ship.GetType().Name,
        tile = ship.occupiedTiles[0].notation
    };
}

public class ShipData
{
    public string orientation;
    public string shipType;
    public string tile;
}