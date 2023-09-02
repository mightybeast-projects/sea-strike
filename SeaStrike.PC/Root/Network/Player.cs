namespace SeaStrike.PC.Root.Network;

public class Player
{
    internal SeaStrike game;
    internal SeaStrikeClient client;
    internal SeaStrikeServer server;

    public Player(SeaStrike game) => this.game = game;

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

    public void SendShipsDeployedMessage() =>
        client.Send(Utils.shipsDeployedMessage);
}