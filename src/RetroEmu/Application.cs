using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroEmu.Devices.DMG;
using RetroEmu.Gui;
using RetroEmu.Wrapper;

namespace RetroEmu;

public class Application : IApplication
{
    private readonly IGui _gui;
    private readonly IGameBoy _gameBoy;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly IApplicationStateProvider _applicationStateProvider;

    public Application(IApplicationStateProvider applicationStateProvider, IGui gui, IGameBoy gameBoy, IWrapper<GraphicsDevice> graphicsDeviceWrapper)
    {
        _applicationStateProvider = applicationStateProvider;
        _gui = gui;
        _gameBoy = gameBoy;
        _graphicsDevice = graphicsDeviceWrapper.Value;
    }
    
    public void Initialize()
    {
        _gui.Initialize();
        
        //var rom = File.ReadAllBytes("/Users/henningtandberg/Downloads/06-ld r,r.gb");
        //_gameBoy.Load(rom);
    }

    public void LoadContent()
    {
        _gui.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        if (_applicationStateProvider.ApplicationState == ApplicationState.Paused)
        {
            return;
        }
        
        //Console.WriteLine("Running");
        //_gameBoy.Update();
    }

    public void Draw(GameTime gameTime)
    {
        _graphicsDevice.Clear(Color.Aqua);
        _gui.Draw(gameTime);
    }
}