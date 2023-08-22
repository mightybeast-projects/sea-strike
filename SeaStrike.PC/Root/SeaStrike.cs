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
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrike : Game
{
    internal static FontSystem fontSystem;
    internal Desktop desktop;
    internal ScreenManager screenManager;

    private readonly GraphicsDeviceManager graphics;

    public SeaStrike()
    {
        graphics = new GraphicsDeviceManager(this);
        screenManager = new ScreenManager();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        MyraEnvironment.Game = this;

        Components.Add(screenManager);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        desktop = new Desktop();

        string path = "SeaStrike.PC/Content/Font/Tektur.ttf";
        byte[] ttf = File.ReadAllBytes(path);
        fontSystem = new FontSystem();
        fontSystem.AddFont(ttf);

        Stylesheet ss = Stylesheet.Current;

        ss.WindowStyle.CloseButtonStyle.Border = new SolidBrush(Color.Red);
        ss.WindowStyle.CloseButtonStyle.BorderThickness = new Thickness(1);
        ss.ComboBoxStyle.LabelStyle.Font = fontSystem.GetFont(24);
        ss.ComboBoxStyle.LabelStyle.Padding = new Thickness(5, 0);

        ImageTextButtonStyle style = ss.ComboBoxStyle.ListBoxStyle.ListItemStyle;
        style.LabelStyle.Font = fontSystem.GetFont(24);
        style.LabelStyle.Padding = new Thickness(5, 0);
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
        catch (Exception e) { ShowErrorDialog(e); }
    }

    private void ShowErrorDialog(Exception e)
    {
        Dialog errorDialog = Dialog.CreateMessageBox("Error",
            e.Message + " " + e.StackTrace);
        errorDialog.Background = new SolidBrush(Color.Black);
        errorDialog.Border = new SolidBrush(Color.Red);
        errorDialog.BorderThickness = new Thickness(1);

        errorDialog.ShowModal(desktop);
    }
}
