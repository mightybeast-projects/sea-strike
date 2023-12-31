﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using SeaStrike.Core.Exceptions;
using SeaStrike.GameCore.Root.Manager;
using SeaStrike.GameCore.Root.Screens;
using SeaStrike.GameCore.Root.Widgets.Modal;

namespace SeaStrike.GameCore.Root;

public class SeaStrikeGame : Game
{
    public static FontManager fontManager;
    public static StringStorage stringStorage;
    public static AudioManager audioManager;
    public Desktop desktop;
    public ScreenManager screenManager;

    private readonly GraphicsManager graphicsManager;

    public SeaStrikeGame()
    {
        fontManager = new FontManager();
        stringStorage = new StringStorage();
        audioManager = new AudioManager(this);
        screenManager = new ScreenManager();
        graphicsManager = new GraphicsManager(this);

        graphicsManager.InitializeGraphics();

        Content.RootDirectory = stringStorage.contentPath;
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        MyraEnvironment.Game = this;

        Components.Add(screenManager);
    }

    protected override void Initialize()
    {
        base.Initialize();

        screenManager.LoadScreen(new MainMenuScreen(this));
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        desktop = new Desktop();

        fontManager.LoadFont();
        audioManager.LoadAudio();

        Stylesheet.Current.LabelStyle.Font = fontManager.GetFont(24);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        GraphicsDevice.Clear(Color.Black);

        try { desktop.Render(); }
        catch (SeaStrikeCoreException e) { ShowCoreLibraryError(e); }
        catch (Exception e) { ShowSystemError(e); }
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        graphicsManager.UpdateScreenViewport();
    }

    private void ShowCoreLibraryError(Exception e) =>
        new ErrorWindow(e.Message).ShowModal(desktop);

    private void ShowSystemError(Exception e) =>
        new ErrorWindow(e.Message + " " + e.StackTrace).ShowModal(desktop);
}
