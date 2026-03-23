using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class RunningState(IEmulatorStateContext emulatorStateContext)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy) =>
        gameBoy.RunAt(frameCounter.CurrentFramesPerSecond);
}