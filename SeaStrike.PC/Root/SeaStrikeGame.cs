using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using SeaStrike.Core.Exceptions;
using SeaStrike.PC.Root.Screens;
using SeaStrike.PC.Root.Widgets.Modal;

namespace SeaStrike.PC.Root;

public class SeaStrikeGame : Game
{
    internal static FontSystem fontSystem;
    internal static StringStorage stringStorage;
    internal Desktop desktop;
    internal ScreenManager screenManager;

    private readonly GraphicsDeviceManager graphics;
    private readonly SeaStrikePlayer player;

    public SeaStrikeGame()
    {
        fontSystem = new FontSystem();
        stringStorage = new StringStorage();
        screenManager = new ScreenManager();
        graphics = new GraphicsDeviceManager(this);
        player = new SeaStrikePlayer(this);

        Content.RootDirectory = stringStorage.contentPath;
        IsMouseVisible = true;
        MyraEnvironment.Game = this;

        Components.Add(screenManager);
    }

    public void ShowVictoryScreen() =>
        new GameOverWindow(player, stringStorage.victoryScreenTitle)
        {
            TitleTextColor = Color.LawnGreen
        }.ShowModal(desktop);

    public void ShowLostScreen() =>
        new GameOverWindow(player, stringStorage.loseScreenTitle)
        {
            TitleTextColor = Color.Red
        }.ShowModal(desktop);

    protected override void LoadContent()
    {
        base.LoadContent();

        desktop = new Desktop();

        string path = stringStorage.fontPath;
        byte[] ttf = File.ReadAllBytes(path);
        fontSystem.AddFont(ttf);

        Stylesheet.Current.LabelStyle.Font = fontSystem.GetFont(24);
    }

    protected override void Initialize()
    {
        base.Initialize();

        screenManager.LoadScreen(new MainMenuScreen(player));
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
