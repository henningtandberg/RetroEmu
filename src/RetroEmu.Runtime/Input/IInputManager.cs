namespace RetroEmu.Runtime.Input;

public interface IInputManager
{
    bool IsButtonDown(EmulatorButton button);
    bool WasButtonJustPressed(EmulatorButton button);
    bool WasButtonJustReleased(EmulatorButton button);
}

public enum EmulatorButton
{
    A,
    B,
    Start,
    Select,
    Up,
    Down,
    Left,
    Right
}
