using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal sealed class RunningState(IApplicationStateContext applicationStateContext)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy) =>
        gameBoy.RunAt(frameCounter.CurrentFramesPerSecond);
}