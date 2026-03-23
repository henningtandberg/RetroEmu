using System.Collections.Generic;
using RetroEmu.Devices.GameBoy;
using RetroEmu.Devices.GameBoy.Input;

namespace RetroEmu.Runtime.Input;

public class InputManager : IInputManager
{
    private readonly HashSet<EmulatorButton> _currentState = new();
    private readonly HashSet<EmulatorButton> _previousState = new();

    public bool IsButtonDown(EmulatorButton button)
    {
        return _currentState.Contains(button);
    }

    public bool WasButtonJustPressed(EmulatorButton button)
    {
        return _currentState.Contains(button) && !_previousState.Contains(button);
    }

    public bool WasButtonJustReleased(EmulatorButton button)
    {
        return !_currentState.Contains(button) && _previousState.Contains(button);
    }

    public void SetButtonState(EmulatorButton button, bool isPressed)
    {
        if (isPressed)
        {
            _currentState.Add(button);
        }
        else
        {
            _currentState.Remove(button);
        }
    }

    public void UpdateFrameState()
    {
        _previousState.Clear();
        foreach (var button in _currentState)
        {
            _previousState.Add(button);
        }
    }

    public void ProcessInput(IGameBoy gameBoy)
    {
        ProcessButton(gameBoy, EmulatorButton.A, Button.A);
        ProcessButton(gameBoy, EmulatorButton.B, Button.B);
        ProcessButton(gameBoy, EmulatorButton.Select, Button.Select);
        ProcessButton(gameBoy, EmulatorButton.Start, Button.Start);

        ProcessDPad(gameBoy, EmulatorButton.Left, DPad.Left);
        ProcessDPad(gameBoy, EmulatorButton.Right, DPad.Right);
        ProcessDPad(gameBoy, EmulatorButton.Up, DPad.Up);
        ProcessDPad(gameBoy, EmulatorButton.Down, DPad.Down);
    }

    private void ProcessButton(IGameBoy gameBoy, EmulatorButton emulatorButton, Button button)
    {
        if (WasButtonJustPressed(emulatorButton))
        {
            gameBoy.ButtonPressed(button);
        }
        if (WasButtonJustReleased(emulatorButton))
        {
            gameBoy.ButtonReleased(button);
        }
    }

    private void ProcessDPad(IGameBoy gameBoy, EmulatorButton emulatorButton, DPad direction)
    {
        if (WasButtonJustPressed(emulatorButton))
        {
            gameBoy.DPadPressed(direction);
        }
        if (WasButtonJustReleased(emulatorButton))
        {
            gameBoy.DPadReleased(direction);
        }
    }
}
