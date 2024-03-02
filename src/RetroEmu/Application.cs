using System;
using System.IO.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroEmu.Devices.DMG;
using RetroEmu.Gui;
using RetroEmu.Wrapper;

namespace RetroEmu;

public class Application : IApplication
{
    private readonly IFileSystem _fileSystem;
    private readonly IConfiguration _configuration;
    private readonly IGui _gui;
    private readonly IGameBoy _gameBoy;
    private readonly GraphicsDevice _graphicsDevice;

    public Application(IFileSystem fileSystem, IConfiguration configuration, IGui gui, IGameBoy gameBoy, IWrapper<GraphicsDevice> graphicsDeviceWrapper)
    {
        _fileSystem = fileSystem;
        _configuration = configuration;
        _gui = gui;
        _gameBoy = gameBoy;
        _graphicsDevice = graphicsDeviceWrapper.Value;
    }
    
    public void Initialize()
    {
        _gui.Initialize();
    }

    public void LoadContent()
    {

        var romPath = _configuration["RomFile"];
        Console.WriteLine($"Reading rom: {romPath}");
        //var rom = _fileSystem.File.ReadAllBytes(romPath);
        //_gameBoy.Load(rom);
        var cartridgeInfo = _gameBoy.GetCartridgeInfo();

        _gui.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        //_gameBoy.Update();
    }

    public void Draw(GameTime gameTime)
    {
        _graphicsDevice.Clear(Color.Aqua);

        _gui.Draw(gameTime);
    }
}