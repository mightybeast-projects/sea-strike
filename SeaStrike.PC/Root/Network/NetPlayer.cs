using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;

namespace SeaStrike.PC.Root.Network;

public class NetPlayer
{
    internal SeaStrike game;
    internal SeaStrikeGame seaStrikeGame;
    internal Board board;
    internal SeaStrikeClient client;
    internal SeaStrikeServer server;
    internal bool canShoot = true;

    internal bool isHost => server is not null;

    public NetPlayer(SeaStrike game) => this.game = game;

    public void UpdateNetwork()
    {
        client?.PollEvents();
        server?.PollEvents();
    }

    public void Disconnect()
    {
        client?.Disconnect();
        server?.Disconnect();
    }

    public void SendBoard(Board board)
    {
        this.board = board;

        client.Send(new BoardData(board).ToJson());
    }

    public void SendShotTile(Tile tile) => client.Send(tile.notation);
}