using System;
using System.IO.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NetExtender.Utilities.Types;
using RetroEmu.Devices.Disassembly;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Gui;
using RetroEmu.Wrapper;

namespace RetroEmu;

public class Application(
    IApplicationStateProvider applicationStateProvider,
    IGui gui,
    IGameBoy gameBoy,
    IDisassembler disassembler,
    IFileSystem fileSystem,
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
        switch (applicationStateProvider.ApplicationState)
        {
            case ApplicationState.Initial:
                return;
            case ApplicationState.Paused:
                break;
            case ApplicationState.LoadRom:
                Console.WriteLine("Loading ROM");
                LoadCartridge(applicationStateProvider.GetSelectedFile());
                applicationStateProvider.ApplicationState = ApplicationState.Running;
                break;
            case ApplicationState.Running:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _frameCounter.Update(gameTime);
        //Console.WriteLine($"FPS: {_frameCounter.CurrentFramesPerSecond}");
        //Console.WriteLine($"FPS Avg.: {_frameCounter.AverageFramesPerSecond}");
        
        HandleKeyboardInput();

        // Iterate until VBlank, with backup in case LCD is turned of. do-while is necessary to jump gameboy out of VBlank on next update
        var currentClockSpeed = gameBoy.GetCurrentClockSpeed();
        var cyclesToRun = currentClockSpeed / _frameCounter.CurrentFramesPerSecond;

        if (applicationStateProvider.ApplicationState == ApplicationState.Paused)
            if (!applicationStateProvider.ShouldStep())
                return;
            else
                cyclesToRun = 0;
        
        var i = 0;
        do
        {
            disassembler.DisassembleNextInstruction();
            gameBoy.Update();
            i++;
            if (i >= 2 * cyclesToRun)
            {
                break;
            }
        } while (!gameBoy.VBlankTriggered());

        //if (gameBoy.GetOutput() != "")
        //{
        //    Console.WriteLine(gameBoy.GetOutput());
        //    System.Diagnostics.Debug.WriteLine(gameBoy.GetOutput());
        //}
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

        // Temp easy windowstuff
        IProcessor processor = gameBoy.GetProcessor();
        var displayColors = new Color[gbWidth * gbHeight];
        for (int y = 0; y < gbHeight; y++)
        {
            for (int x = 0; x < gbWidth; x++)
            {
                var inColor = processor.GetDisplayColor(x, y);
                var index = y * gbWidth + x;
                Color outColor = new Color(1.0f, 1.0f, 1.0f);
                
                if (inColor == 1)
                {
                    outColor = new Color(0.66f, 0.66f, 0.66f);
                }
                else if (inColor == 2)
                {
                    outColor = new Color(0.33f, 0.33f, 0.33f);
                }
                else if (inColor == 3)
                {
                    outColor = new Color(0.0f, 0.0f, 0.0f);
                }
                displayColors[index] = outColor;
            }
        }
        _displayTexture.SetData(displayColors);

        _spriteBatch.Begin(samplerState: SamplerState.AnisotropicClamp);
        _spriteBatch.Draw(_gameBoyWindowSprite, _gameBoyWindowSpritePosition, Color.White);
        _spriteBatch.Draw(_displayTexture, new Rectangle(75, 57, (int)(1.67f * gbWidth), (int)(1.67f * gbHeight)), Color.White);
        _spriteBatch.DrawString(_gameBoyFont, "Retro Emu", new Vector2(110, 330), Color.LightGray);
        _spriteBatch.End();

        gui.Draw(gameTime);
    }

    private void LoadCartridge(string selectedFile)
    {
        if (!fileSystem.File.Exists(selectedFile))
        {
            return;
        }
        
        gameBoy.Reset();
        
        var cartridgeMemory = fileSystem.File.ReadAllBytes(selectedFile);
        gameBoy.Load(cartridgeMemory);
    }
}