using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.UI.Desktop.State;

internal sealed class PausedState(IApplicationStateContext applicationStateContext)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    {
        // Nothing to be done
    }
}