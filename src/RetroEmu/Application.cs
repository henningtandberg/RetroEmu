using System;
using System.IO.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroEmu.Devices.DMG;
using RetroEmu.Gui;
using RetroEmu.Wrapper;

namespace RetroEmu;

public class Application(
    IApplicationStateProvider applicationStateProvider,
    IGui gui,
    IGameBoy gameBoy,
    IFileSystem fileSystem,
    IWrapper<GraphicsDevice> graphicsDeviceWrapper)
    : IApplication
{
    private readonly GraphicsDevice _graphicsDevice = graphicsDeviceWrapper.Value;
    private FrameCounter _frameCounter = new();

    public void Initialize()
    {
        gui.Initialize();
    }

    public void LoadContent()
    {
        gui.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        switch (applicationStateProvider.ApplicationState)
        {
            case ApplicationState.Paused:
                return;
            case ApplicationState.LoadRom:
                Console.WriteLine("Loading ROM");
                LoadRom(applicationStateProvider.GetSelectedFile());
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
        
        var currentClockSpeed = gameBoy.GetCurrentClockSpeed();
        var cyclesToRun = currentClockSpeed / _frameCounter.CurrentFramesPerSecond;
        for (var i = 0; i < cyclesToRun; i++)
        {
            gameBoy.Update();
        }
        Console.WriteLine(gameBoy.GetOutput());
    }

    public void Draw(GameTime gameTime)
    {
        _graphicsDevice.Clear(Color.Aqua);
        gui.Draw(gameTime);
    }

    private void LoadRom(string selectedFile)
    {
        if (!fileSystem.File.Exists(selectedFile))
        {
            return;
        }
        
        gameBoy.Reset();
        
        var rom = fileSystem.File.ReadAllBytes(selectedFile);
        gameBoy.Load(rom);
    }
}