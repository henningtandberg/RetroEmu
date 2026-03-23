using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.Input;

public interface IInputManager
{
    bool IsButtonDown(EmulatorButton button);
    bool WasButtonJustPressed(EmulatorButton button);
    bool WasButtonJustReleased(EmulatorButton button);

    /// <summary>
    /// Process input state and send input events to the GameBoy
    /// </summary>
    void ProcessInput(IGameBoy gameBoy);
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
