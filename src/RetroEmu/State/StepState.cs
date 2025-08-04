using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal sealed class StepState(IApplicationStateContext applicationStateContext)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    {
        gameBoy.Update();
        _applicationStateContext.Pause();
    }
}