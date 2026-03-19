using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.UI.Desktop.State;

internal sealed class RunningState(IApplicationStateContext applicationStateContext)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy) =>
        gameBoy.RunAt(frameCounter.CurrentFramesPerSecond);
}