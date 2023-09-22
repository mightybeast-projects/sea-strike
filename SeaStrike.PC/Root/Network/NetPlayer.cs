using Newtonsoft.Json;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Network.Listener;
using SeaStrike.PC.Root.Network.Manager;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Screens.Multiplayer;

namespace SeaStrike.PC.Root.Network;

public class NetPlayer : SeaStrikePlayer
{
    internal bool canShoot => game.currentPlayer.board == board;
    internal bool isHost => server is not null;

    private SeaStrikeClient client;
    private SeaStrikeServer server;
    private BoardData opponentBoardData;

    public NetPlayer(SeaStrikeGame seaStrikeGame) : base(seaStrikeGame) { }

    public new void StartCoreGame()
    {
        if (isHost)
            game = new Game(board, opponentBoardData.Build());
        else
            game = new Game(board, opponentBoardData.Build(), true);
    }

    public void CreateServer()
    {
        server = new SeaStrikeServer(new SeaStrikeServerListener(this));

        server.Start();
    }

    public void CreateClient()
    {
        client = new SeaStrikeClient(new SeaStrikeClientListener(this));

        client.Start();
    }

    public void UpdateNetManagers()
    {
        client?.PollEvents();
        server?.PollEvents();
    }

    public void Disconnect()
    {
        client?.DisconnectAll();
        server?.DisconnectAll();

        client?.Stop();
        server?.Stop();

        client = null;
        server = null;

        RedirectTo<MainMenuScreen>();
    }

    public void SendBoard() => client.Send(new BoardData(board).ToJson());

    public void ReceiveOpponentBoardData(string opponentBoardDataJson) =>
        opponentBoardData =
            JsonConvert.DeserializeObject<BoardData>(opponentBoardDataJson);

    public void SendShotTile(Tile tile) => client.Send(tile.notation);

    public void HandleOpponentShot(string tileStr)
    {
        game.HandleCurrentPlayerShot(tileStr);

        if (game.isOver)
            ShowLostScreen();
    }
}