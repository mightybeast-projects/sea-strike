using Newtonsoft.Json;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Screens.Multiplayer;

namespace SeaStrike.PC.Root.Network;

public class NetPlayer
{
    internal readonly SeaStrike game;
    internal SeaStrikeGame seaStrikeGame;
    internal Board board;

    internal bool canShoot => seaStrikeGame.currentPlayer.board == board;
    internal bool isHost => server is not null;

    private SeaStrikeClient client;
    private SeaStrikeServer server;
    private BoardData opponentBoardData;

    public NetPlayer(SeaStrike game) => this.game = game;

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
        seaStrikeGame.HandleCurrentPlayerShot(tileStr);

        if (seaStrikeGame.isOver)
            game.ShowLostScreen();
    }

    public void RedirectToMainMenu() =>
        game.screenManager.LoadScreen(new MainMenuScreen(game));

    public void RedirectToDeploymentScreen() =>
        game.screenManager.LoadScreen(
            new MultiplayerDeploymentPhaseScreen(this));

    public void RedirectToBattleScreen() =>
        game.screenManager.LoadScreen(
            new MultiplayerBattlePhaseScreen(this, opponentBoardData.Build()));
}