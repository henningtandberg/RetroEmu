using System.Collections.Generic;

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
}
