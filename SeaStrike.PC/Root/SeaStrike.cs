using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using SeaStrike.Core.Exceptions;
using SeaStrike.PC.Root.Screens;
using GameWindow = SeaStrike.PC.Root.Widgets.Modal.GameWindow;

namespace SeaStrike.PC.Root;

public class SeaStrike : Game
{
    internal static FontSystem fontSystem;
    internal static StringStorage stringStorage = new StringStorage();
    internal Desktop desktop;
    internal ScreenManager screenManager;

    private readonly GraphicsDeviceManager graphics;

    public SeaStrike()
    {
        graphics = new GraphicsDeviceManager(this);
        screenManager = new ScreenManager();

        Content.RootDirectory = stringStorage.contentPath;
        IsMouseVisible = true;
        MyraEnvironment.Game = this;

        Components.Add(screenManager);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        desktop = new Desktop();

        InitializeFontSystem();
        ApplyGlobalStylesheet();
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

    private void InitializeFontSystem()
    {
        string path = stringStorage.fontPath;
        byte[] ttf = File.ReadAllBytes(path);
        fontSystem = new FontSystem();
        fontSystem.AddFont(ttf);
    }

    private void ApplyGlobalStylesheet()
    {
        Stylesheet ss = Stylesheet.Current;

        ss.LabelStyle.Font = SeaStrike.fontSystem.GetFont(24);

        ss.ComboBoxStyle.LabelStyle.Font = fontSystem.GetFont(24);
        ss.ComboBoxStyle.LabelStyle.Padding = new Thickness(5, 0);

        ImageTextButtonStyle style = ss.ComboBoxStyle.ListBoxStyle.ListItemStyle;
        style.LabelStyle.Font = fontSystem.GetFont(24);
        style.LabelStyle.Padding = new Thickness(5, 0);
    }

    private void ShowCoreLibraryError(Exception e) =>
        ShowErrorWindow(e.Message);

    private void ShowSystemError(Exception e) =>
        ShowErrorWindow(e.Message + " " + e.StackTrace);

    private void ShowErrorWindow(string message) =>
        new GameWindow(stringStorage.errorWindowTitle)
        {
            Content = new Label() { Text = message },
            TitleTextColor = Color.Red,
            Border = new SolidBrush(Color.Red),
            DragHandle = null
        }.ShowModal(desktop);
}
