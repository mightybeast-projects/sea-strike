using Microsoft.Xna.Framework;
using SeaStrike.PC.Root.Network;
using SeaStrike.PC.Root.Widgets.Modal;

namespace SeaStrike.PC.Root.Screens.Multiplayer;

public class NetDeploymentPhaseScreen : DeploymentPhaseScreen
{
    private NetPlayer player;

    public NetDeploymentPhaseScreen(NetPlayer player)
        : base(player.game)
            => this.player = player;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetManagers();
    }

    protected override void OnBackButtonPressed() =>
        new DisconnectionWarningWindow(player.Disconnect)
            .ShowModal(game.desktop);

    protected override void OnStartButtonPressed()
    {
        player.SendBoard(boardBuilder.Build());

        new ReadyWindow().ShowModal(game.desktop);
    }
}