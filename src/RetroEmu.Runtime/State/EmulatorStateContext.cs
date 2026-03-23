using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

public sealed class EmulatorStateContext : IEmulatorStateContext
{
    private IEmulatorState _state;

    public EmulatorStateContext()
    {
        _state = new InitialState(this);
    }

    public void SetState(IEmulatorState nextState)
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

    public void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy) => _state.Update(frameCounter, gameBoy);
}