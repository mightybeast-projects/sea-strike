using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Newtonsoft.Json;
using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Network;

public class BoardData
{
    public List<ShipData> shipDatas;

    private BoardBuilder boardBuilder;
    private string coreAsseblyName = "SeaStrike.Core";
    private string shipsNamespace = "SeaStrike.Core.Entity.";

    public BoardData(Board board)
    {
        shipDatas = new List<ShipData>();

        foreach (Ship ship in board.ships)
            shipDatas.Add(NewShipData(ship));
    }

    [JsonConstructor]
    public BoardData(ShipData[] shipDatas) =>
        this.shipDatas = shipDatas.ToList();

    public Board Build()
    {
        boardBuilder = new BoardBuilder();

        foreach (ShipData data in shipDatas)
            AddShipFromData(data);

        return boardBuilder.Build();
    }

    public string ToJson() => JsonConvert.SerializeObject(this);

    private Func<Ship, ShipData> NewShipData = (Ship ship) => new ShipData()
    {
        orientation = ship.orientation.ToString(),
        shipType = ship.GetType().Name,
        tile = ship.occupiedTiles[0].notation
    };

    private void AddShipFromData(ShipData data)
    {
        Orientation shipOrientation =
                        Enum.Parse<Orientation>(data.orientation);

        ObjectHandle container =
            Activator.CreateInstance(
                coreAsseblyName,
                shipsNamespace + data.shipType);
        Ship ship = (Ship)container.Unwrap();

        Func<Ship, BoardBuilder> AddShip =
            shipOrientation == Orientation.Horizontal ?
                boardBuilder.AddHorizontalShip :
                boardBuilder.AddVerticalShip;

        AddShip(ship).AtPosition(data.tile);
    }
}

public class ShipData
{
    public string orientation;
    public string shipType;
    public string tile;

    public ShipData() { }

    [JsonConstructor]
    public ShipData(string orientation, string shipType, string tile)
    {
        this.orientation = orientation;
        this.shipType = shipType;
        this.tile = tile;
    }
}