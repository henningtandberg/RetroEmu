using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class PausedState(IEmulatorStateContext emulatorStateContext)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    {
        // Nothing to be done
    }
}