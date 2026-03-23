using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal abstract class BaseEmulatorState(IEmulatorStateContext emulatorStateContext) : IEmulatorState
{
    protected IEmulatorStateContext _emulatorStateContext = emulatorStateContext;
    public void HandleStart() =>
        _emulatorStateContext.SetState(new RunningState(_emulatorStateContext));

    public void HandlePause() =>
        _emulatorStateContext.SetState(new PausedState(_emulatorStateContext));

    public void HandleLoad(byte[] cartridgeData) =>
        _emulatorStateContext.SetState(new LoadState(_emulatorStateContext, cartridgeData));

    public void HandleStep() =>
        _emulatorStateContext.SetState(new StepState(_emulatorStateContext));

    public abstract void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy);
}