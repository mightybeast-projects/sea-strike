﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using SeaStrike.PC.Root.Screens;

namespace SeaStrike.PC.Root;

public class SeaStrike : Game
{
    internal SpriteBatch spriteBatch;
    private GraphicsDeviceManager graphics;
    private ScreenManager screenManager;

    public SeaStrike()
    {
        graphics = new GraphicsDeviceManager(this);
        screenManager = new ScreenManager();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Components.Add(screenManager);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Initialize()
    {
        base.Initialize();

        screenManager.LoadScreen(new MainMenuScreen(this));
    }
}
