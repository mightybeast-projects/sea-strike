using Newtonsoft.Json;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.GameLogic;
using SeaStrike.PC.Root.Network.Listener;
using SeaStrike.PC.Root.Network.Manager;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Screens.Multiplayer;

namespace SeaStrike.PC.Root.Network;

public class NetPlayer
{
    internal readonly SeaStrikeGame seaStrikeGame;
    internal Game game;
    internal Board board;

    internal bool canShoot => game.currentPlayer.board == board;
    internal bool isHost => server is not null;

    private SeaStrikeClient client;
    private SeaStrikeServer server;
    private BoardData opponentBoardData;

    public NetPlayer(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void CreateServer()
    {
        var server = new SeaStrikeServer(new SeaStrikeServerListener(this));

        this.server = server;

        server.Start();
    }

    public void CreateClient()
    {
        var client = new SeaStrikeClient(new SeaStrikeClientListener(this));

        this.client = client;

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
        server?.Stop();
    }

    public void SendBoard(Board board)
    {
        this.board = board;

        client.Send(new BoardData(board).ToJson());
    }

    public void ReceiveOpponentBoardData(string opponentBoardDataJson) =>
        opponentBoardData =
            JsonConvert.DeserializeObject<BoardData>(opponentBoardDataJson);

    public void SendShotTile(Tile tile) => client.Send(tile.notation);

    public void HandleOpponentShot(string tileStr)
    {
        game.HandleCurrentPlayerShot(tileStr);

        if (game.isOver)
            seaStrikeGame.ShowLostScreen();
    }

    public void RedirectToMainMenu() =>
        seaStrikeGame.screenManager.LoadScreen(
            new MainMenuScreen(seaStrikeGame));

    public void RedirectToNetDeploymentScreen() =>
        seaStrikeGame.screenManager.LoadScreen(
            new NetDeploymentPhaseScreen(this));

    public void RedirectToNetBattleScreen() =>
        seaStrikeGame.screenManager.LoadScreen(
            new NetBattlePhaseScreen(this, opponentBoardData.Build()));
}