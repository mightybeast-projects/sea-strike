using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeaStrike.GameCore.Root.Manager;

public class GraphicsManager
{
    private readonly SeaStrikeGame seaStrikeGame;

    private GraphicsDeviceManager graphics;

    public GraphicsManager(SeaStrikeGame seaStrikeGame) =>
        this.seaStrikeGame = seaStrikeGame;

    public void InitializeGraphics()
    {
        graphics = new GraphicsDeviceManager(seaStrikeGame);

        if (OperatingSystem.IsAndroid())
            InitializeAndroidGraphics();
        else
            InitializeDesktopGraphics();

        graphics.ApplyChanges();
    }

    public void UpdateScreenViewport()
    {
        if (seaStrikeGame.GraphicsDevice.Viewport.Width < 800)
            graphics.PreferredBackBufferWidth = 800;
        if (seaStrikeGame.GraphicsDevice.Viewport.Height < 480)
            graphics.PreferredBackBufferHeight = 480;

        graphics.ApplyChanges();
    }

    private void InitializeAndroidGraphics()
    {
        graphics.IsFullScreen = true;
        graphics.PreferredBackBufferWidth =
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight =
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        graphics.SupportedOrientations =
            DisplayOrientation.LandscapeLeft |
            DisplayOrientation.LandscapeRight;
    }

    private void InitializeDesktopGraphics()
    {
        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
    }
}