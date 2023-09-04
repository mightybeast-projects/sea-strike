using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Network;
using GameWindow = SeaStrike.PC.Root.Widgets.Modal.GameWindow;

namespace SeaStrike.PC.Root.Screens;

public class MultiplayerDeploymentPhaseScreen : DeploymentPhaseScreen
{
    private Player player;
    private SeaStrike game => player.game;

    public MultiplayerDeploymentPhaseScreen(Player player) : base(player.game)
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

        GameWindow readyWindow =
            new GameWindow(SeaStrike.stringStorage.readyWindowTitle)
            {
                Content = new Label()
                {
                    Text = SeaStrike.stringStorage.readyWindowContentLabel
                },
                DragHandle = null,
            };

        readyWindow.CloseButton.Visible = false;

        readyWindow.ShowModal(game.desktop);
    }
}