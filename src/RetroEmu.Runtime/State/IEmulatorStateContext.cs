using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

/// <summary>
/// This controls the emulator state and makes sure transitions between states
/// happen in a predictable and understandable way.
/// </summary>
public interface IEmulatorStateContext
{
    /// <summary>
    /// Sets the new state of the emulator
    /// </summary>
    /// <param name="nextState"></param>
    public void SetState(IEmulatorState nextState);

    public bool IsInitialState();

    public bool IsPaused();
    
    bool IsRunningState();
    
    /// <summary>
    /// When called from the outside (GUI), it will transition to the Running state
    /// </summary>
    public void Start();
    
    /// <summary>
    /// When called from the outside (GUI), it will transition to the Paused state
    /// </summary>
    public void Pause();

    public void Load(byte[] cartridgeData);

    public void Step();
    
    public void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy);
}