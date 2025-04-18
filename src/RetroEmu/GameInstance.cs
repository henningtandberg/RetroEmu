﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RetroEmu;

public class GameInstance : Game, IGame
{
    private GraphicsDeviceManager _graphics;
    private IApplication _application;
    
    private readonly IServiceProvider _serviceProvider;

    public GameInstance(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1200,
            PreferredBackBufferHeight = 800,
            PreferMultiSampling = true
        };
    }

    protected override void Initialize()
    {
        Content.RootDirectory = "Content";
        Window.AllowUserResizing = true;
        IsMouseVisible = true;

        _application = _serviceProvider.GetRequiredService<IApplication>();
        _application.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {

        _application.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _application.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _application.Draw(gameTime);
        base.Draw(gameTime);
    }
}