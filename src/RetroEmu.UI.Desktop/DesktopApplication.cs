using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroEmu.Runtime;
using RetroEmu.Runtime.Input;
using RetroEmu.UI.Desktop.Gui;

namespace RetroEmu.UI.Desktop;

public class DesktopApplication : Game, IGame
{
    private GraphicsDeviceManager _graphics;
    private IEmulatorOrchestrator _emulatorOrchestrator;
    private InputManager _inputManager;
    private IGui _gui;

    private readonly IServiceProvider _serviceProvider;

    public DesktopApplication(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1200,
            PreferredBackBufferHeight = 800,
            // Fix run for linux/debian
            PreferMultiSampling = false
        };
    }

    protected override void Initialize()
    {
        Content.RootDirectory = "Content";
        Window.AllowUserResizing = true;
        IsMouseVisible = true;

        _emulatorOrchestrator = _serviceProvider.GetRequiredService<IEmulatorOrchestrator>();
        _inputManager = _serviceProvider.GetRequiredService<InputManager>();
        _gui = _serviceProvider.GetRequiredService<IGui>();

        _emulatorOrchestrator.Initialize();
        _gui.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _gui.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        UpdateInputManager();
        _emulatorOrchestrator.Update(gameTime.ElapsedGameTime);
        _inputManager.UpdateFrameState();

        base.Update(gameTime);
    }

    private void UpdateInputManager()
    {
        var keyboardState = Keyboard.GetState();

        _inputManager.SetButtonState(EmulatorButton.A, keyboardState.IsKeyDown(Keys.A));
        _inputManager.SetButtonState(EmulatorButton.B, keyboardState.IsKeyDown(Keys.S));
        _inputManager.SetButtonState(EmulatorButton.Select, keyboardState.IsKeyDown(Keys.Q));
        _inputManager.SetButtonState(EmulatorButton.Start, keyboardState.IsKeyDown(Keys.W));

        _inputManager.SetButtonState(EmulatorButton.Left, keyboardState.IsKeyDown(Keys.Left));
        _inputManager.SetButtonState(EmulatorButton.Right, keyboardState.IsKeyDown(Keys.Right));
        _inputManager.SetButtonState(EmulatorButton.Up, keyboardState.IsKeyDown(Keys.Up));
        _inputManager.SetButtonState(EmulatorButton.Down, keyboardState.IsKeyDown(Keys.Down));
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Aqua);
        _gui.Draw(gameTime);
        base.Draw(gameTime);
    }

    public event EventHandler<EventArgs> Exiting;
}