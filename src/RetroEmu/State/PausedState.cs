using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal sealed class PausedState(IApplicationStateContext applicationStateContext)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    {
        // Nothing to be done
    }
}