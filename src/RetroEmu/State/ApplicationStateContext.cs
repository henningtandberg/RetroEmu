using System;
using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal sealed class ApplicationStateContext : IApplicationStateContext
{
    private IApplicationState _state;

    public ApplicationStateContext()
    {
        _state = new InitialState(this);
    }

    public void SetState(IApplicationState nextState)
    {
        Console.WriteLine("Entering " + nextState.GetType());
        _state = nextState;
    }

    public bool IsInitialState() => _state is InitialState;
    
    public bool IsPaused() => _state is PausedState;
    
    public bool IsRunningState() => _state is RunningState;

    public void Start() => _state.HandleStart();

    public void Pause() => _state.HandlePause();

    public void Load(byte[] cartridgeData) => _state.HandleLoad(cartridgeData);

    public void Step() => _state.HandleStep();

    public void Update(IFrameCounter frameCounter, IGameBoy gameBoy) => _state.Update(frameCounter, gameBoy);
}