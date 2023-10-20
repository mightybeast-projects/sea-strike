using Newtonsoft.Json;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.Core.Entity.GameLogic.Utility;
using SeaStrike.GameCore.Root.Network.Listener;
using SeaStrike.GameCore.Root.Network.Manager;
using SeaStrike.GameCore.Root.Screens;

namespace SeaStrike.GameCore.Root.Network;

public class NetPlayer : SeaStrikePlayer
{
    public bool canShoot => game.currentPlayer.board == board;
    public bool isHost => server is not null;
    public bool canCreateServer = true;

    protected override Action onGameOverScreenExitButtonClicked => Disconnect;

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
        DiscoverServer();

        server = new SeaStrikeServer(new SeaStrikeServerListener(this));

        server.Start();
    }

    public void CreateClient()
    {
        client = new SeaStrikeClient(new SeaStrikeClientListener(this));

        client.Start();
    }

    public void DiscoverServer() => client.SendBroadcast(
        new SeaStrikeNetDataWriter(NetUtils.serverDiscoveryMessage),
        NetUtils.port);

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
        ShotResult result = game.HandleCurrentPlayerShot(tileStr);

        SeaStrikeGame.audioManager.PlayShotResultSFX(result);

        if (game.isOver)
            ShowLostScreen();
    }
}