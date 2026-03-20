using System;
using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;
using RetroEmu.Runtime.Input;
using RetroEmu.Runtime.State;

namespace RetroEmu.Runtime;

public class Application(
    IGameBoy gameBoy,
    IApplicationStateContext stateContext,
    IInputManager inputManager)
    : IApplication
{
    private readonly FrameCounter _frameCounter = new();

    public void Initialize()
    {
    }

    public void Update(TimeSpan deltaTime)
    {
        _frameCounter.Update(deltaTime);
        HandleInput();
        stateContext.Update(_frameCounter, gameBoy);
    }

    private void HandleInput()
    {
        HandleButton(EmulatorButton.A, Button.A);
        HandleButton(EmulatorButton.B, Button.B);
        HandleButton(EmulatorButton.Select, Button.Select);
        HandleButton(EmulatorButton.Start, Button.Start);

        HandleDPad(EmulatorButton.Left, DPad.Left);
        HandleDPad(EmulatorButton.Right, DPad.Right);
        HandleDPad(EmulatorButton.Up, DPad.Up);
        HandleDPad(EmulatorButton.Down, DPad.Down);
    }

    private void HandleDPad(EmulatorButton emulatorButton, DPad direction)
    {
        if (inputManager.WasButtonJustPressed(emulatorButton))
        {
            gameBoy.DPadPressed(direction);
        }
        if (inputManager.WasButtonJustReleased(emulatorButton))
        {
            gameBoy.DPadReleased(direction);
        }
    }

    private void HandleButton(EmulatorButton emulatorButton, Button button)
    {
        if (inputManager.WasButtonJustPressed(emulatorButton))
        {
            gameBoy.ButtonPressed(button);
        }
        if (inputManager.WasButtonJustReleased(emulatorButton))
        {
            gameBoy.ButtonReleased(button);
        }
    }
}