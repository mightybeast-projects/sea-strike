using Microsoft.Xna.Framework;
using SeaStrike.GameCore.Root.Network;
using SeaStrike.GameCore.Root.Widgets.Modal;

namespace SeaStrike.GameCore.Root.Screens.Multiplayer;

public class NetDeploymentPhaseScreen : DeploymentPhaseScreen
{
    private new NetPlayer player => (NetPlayer)base.player;

    public NetDeploymentPhaseScreen(NetPlayer player) : base(player) { }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        player.UpdateNetManagers();
    }

    protected override void OnBackButtonPressed() =>
        new DisconnectionWarningWindow(player.Disconnect)
            .ShowModal(seaStrikeGame.desktop);

    protected override void OnStartButtonPressed()
    {
        player.SendBoard();

        new ReadyWindow().ShowModal(seaStrikeGame.desktop);
    }
}