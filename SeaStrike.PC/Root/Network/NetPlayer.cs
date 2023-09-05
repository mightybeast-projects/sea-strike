using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Network;

public class NetPlayer
{
    internal SeaStrike game;
    internal Board board;
    internal SeaStrikeClient client;
    internal SeaStrikeServer server;

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
}