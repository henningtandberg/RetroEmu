using System;
using System.IO.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Gui;
using RetroEmu.State;
using RetroEmu.Wrapper;

namespace RetroEmu;

public class Application(
    IGui gui,
    IGameBoy gameBoy,
    IApplicationStateContext stateContext,
    IWrapper<GraphicsDevice> graphicsDeviceWrapper,
    IWrapper<ContentManager> contentManagerWrapper)
    : IApplication
{
    private readonly GraphicsDevice _graphicsDevice = graphicsDeviceWrapper.Value;
    private readonly ContentManager _contentManager = contentManagerWrapper.Value;
    
    private SpriteBatch _spriteBatch;
    private FrameCounter _frameCounter = new();
    private Texture2D _displayTexture;
    private SpriteFont _gameBoyFont;
    private Texture2D _gameBoyWindowSprite;
    private Vector2 _gameBoyWindowSpritePosition;

    private int gbWidth = 160;
    private int gbHeight = 144;

    public void Initialize()
    {
        gui.Initialize();
        _spriteBatch = new SpriteBatch(_graphicsDevice);
        _displayTexture = new Texture2D(_graphicsDevice, gbWidth, gbHeight);
    }

    public void LoadContent()
    {
        gui.LoadContent();

        _gameBoyFont = _contentManager.Load<SpriteFont>("GameBoy1989");
        _gameBoyWindowSprite = _contentManager.Load<Texture2D>("gbc-screen");
        _gameBoyWindowSpritePosition = new Vector2(0, 15);
    }

    private KeyboardState _previousState;
    public void Update(GameTime gameTime)
    {
        _frameCounter.Update(gameTime);
        HandleKeyboardInput();
        stateContext.Update(_frameCounter, gameBoy);
    }

    private void HandleKeyboardInput()
    {
        var state = Keyboard.GetState();

        // Move our sprite based on arrow keys being pressed:
        ButtonPressed(state, Keys.A, Button.A);
        ButtonPressed(state, Keys.S, Button.B);
        ButtonPressed(state, Keys.Q, Button.Select);
        ButtonPressed(state, Keys.W, Button.Start);
        
        DPadPressed(state, Keys.Left, DPad.Left);
        DPadPressed(state, Keys.Right, DPad.Right);
        DPadPressed(state, Keys.Up, DPad.Up);
        DPadPressed(state, Keys.Down, DPad.Down);
        
        _previousState = state;
    }

    private void DPadPressed(KeyboardState state, Keys key, DPad direction)
    {
        if (state.IsKeyDown(key) & !_previousState.IsKeyDown(key))
        {
            gameBoy.DPadPressed(direction);
        }
        if (!state.IsKeyDown(key) & _previousState.IsKeyDown(key))
        {
            gameBoy.DPadReleased(direction);
        }
    }

    private void ButtonPressed(KeyboardState state, Keys key, Button button)
    {
        if (state.IsKeyDown(key) & !_previousState.IsKeyDown(key))
        {
            gameBoy.ButtonPressed(button);
        }
        if (!state.IsKeyDown(key) & _previousState.IsKeyDown(key))
        {
            gameBoy.ButtonReleased(button);
        }
    }

    public void Draw(GameTime gameTime)
    {
        _graphicsDevice.Clear(Color.Aqua);
        gui.Draw(gameTime);
    }
}