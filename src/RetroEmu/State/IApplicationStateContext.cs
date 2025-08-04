using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

/// <summary>
/// This controls the application state and makes sure transitions between states
/// happen in a predictable and understandable way.
/// </summary>
public interface IApplicationStateContext
{
    /// <summary>
    /// Sets the new state of the application
    /// </summary>
    /// <param name="nextState"></param>
    public void SetState(IApplicationState nextState);

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
    
    public void Update(IFrameCounter frameCounter, IGameBoy gameBoy);
}