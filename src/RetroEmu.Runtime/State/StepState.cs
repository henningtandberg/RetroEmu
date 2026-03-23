using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class StepState(IEmulatorStateContext emulatorStateContext)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    {
        gameBoy.Update();
        _emulatorStateContext.Pause();
    }
}