using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using SeaStrike.Core.Exceptions;
using SeaStrike.GameCore.Root.Screens;
using SeaStrike.GameCore.Root.Widgets.Modal;

namespace SeaStrike.GameCore.Root;

public class SeaStrikeGame : Game
{
    public static FontSystem fontSystem;
    public static StringStorage stringStorage;
    public Desktop desktop;
    public ScreenManager screenManager;

    private readonly GraphicsDeviceManager graphics;

    public SeaStrikeGame()
    {
        fontSystem = new FontSystem();
        stringStorage = new StringStorage();
        screenManager = new ScreenManager();
        graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = stringStorage.contentPath;
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        MyraEnvironment.Game = this;

        Components.Add(screenManager);

        if (OperatingSystem.IsAndroid())
            graphics.IsFullScreen = true;
        else
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        desktop = new Desktop();

        string path = OperatingSystem.IsAndroid() ?
            stringStorage.androidFontPath : stringStorage.pcFontPath;
        byte[] ttf;
        using (var stream = TitleContainer.OpenStream(path))
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                ttf = ms.ToArray();
            }
        }
        fontSystem.AddFont(ttf);

        Stylesheet.Current.LabelStyle.Font = fontSystem.GetFont(24);
    }

    protected override void Initialize()
    {
        base.Initialize();

        screenManager.LoadScreen(new MainMenuScreen(this));
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        GraphicsDevice.Clear(Color.Black);

        try { desktop.Render(); }
        catch (SeaStrikeCoreException e) { ShowCoreLibraryError(e); }
        catch (Exception e) { ShowSystemError(e); }
    }

    private void ShowCoreLibraryError(Exception e) =>
        new ErrorWindow(e.Message).ShowModal(desktop);

    private void ShowSystemError(Exception e) =>
        new ErrorWindow(e.Message + " " + e.StackTrace).ShowModal(desktop);
}
