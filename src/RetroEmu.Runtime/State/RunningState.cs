using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class RunningState(IEmulatorStateContext emulatorStateContext)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy) =>
        gameBoy.RunAt(frameCounter.CurrentFramesPerSecond);
}