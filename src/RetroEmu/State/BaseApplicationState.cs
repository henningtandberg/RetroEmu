using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal abstract class BaseApplicationState(IApplicationStateContext applicationStateContext) : IApplicationState
{
    protected IApplicationStateContext _applicationStateContext = applicationStateContext;
    public void HandleStart() =>
        _applicationStateContext.SetState(new RunningState(_applicationStateContext));

    public void HandlePause() =>
        _applicationStateContext.SetState(new PausedState(_applicationStateContext));

    public void HandleLoad(byte[] cartridgeData) =>
        _applicationStateContext.SetState(new LoadState(_applicationStateContext, cartridgeData));

    public void HandleStep() =>
        _applicationStateContext.SetState(new StepState(_applicationStateContext));

    public abstract void Update(IFrameCounter frameCounter, IGameBoy gameBoy);
}