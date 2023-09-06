using Newtonsoft.Json;
using SeaStrike.Core.Entity;
using SeaStrike.Core.Entity.Game;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Screens.Multiplayer;

namespace SeaStrike.PC.Root.Network;

public class NetPlayer
{
    internal SeaStrike game;
    internal SeaStrikeGame seaStrikeGame;
    internal Board board;
    internal SeaStrikeClient client;
    internal SeaStrikeServer server;

    private BoardData opponentBoardData;

    internal bool canShoot => seaStrikeGame.currentPlayer.board == board;
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