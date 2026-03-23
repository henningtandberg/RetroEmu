using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class PausedState(IEmulatorStateContext emulatorStateContext)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy)
    {
        // Nothing to be done
    }
}