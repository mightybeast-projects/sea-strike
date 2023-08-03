﻿using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Myra;
using Myra.Graphics2D.UI;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrike : Game
{
    internal Desktop desktop;
    internal ScreenManager screenManager;
    internal SpriteBatch spriteBatch;
    internal FontSystem fontSystem;


    public SeaStrike()
    {
        screenManager = new ScreenManager();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        MyraEnvironment.Game = this;

        Components.Add(screenManager);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        spriteBatch = new SpriteBatch(GraphicsDevice);
        desktop = new Desktop();

        string path = "SeaStrike.PC/Content/Font/ComicSans.ttf";
        byte[] ttf = File.ReadAllBytes(path);
        fontSystem = new FontSystem();
        fontSystem.AddFont(ttf);
    }

    protected override void Initialize()
    {
        base.Initialize();

        screenManager.LoadScreen(new MainMenuScreen(this));
    }
}
