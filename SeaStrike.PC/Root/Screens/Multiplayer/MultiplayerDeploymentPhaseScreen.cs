using Microsoft.Xna.Framework;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.Modal;

namespace SeaStrike.PC.Root.Screens.Multiplayer;

public class MultiplayerDeploymentPhaseScreen : DeploymentPhaseScreen
{
    private NetPlayer player;
    private SeaStrike game => player.game;

    public MultiplayerDeploymentPhaseScreen(NetPlayer player) : base(player.game)
        => this.player = player;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetwork();
    }

    protected override void OnBackButtonPressed()
    {
        player.Disconnect();

        game.screenManager.LoadScreen(new MainMenuScreen(game));
    }

    protected override void OnStartButtonPressed()
    {
        player.SendBoard(boardBuilder.Build());

        new ReadyWindow().ShowModal(game.desktop);
    }
}