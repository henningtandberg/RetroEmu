using RetroEmu.Abstractions;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class StepState(IEmulatorStateContext emulatorStateContext)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IReadOnlyFrameCounter frameCounter, IGameBoy gameBoy)
    {
        gameBoy.Update();
        _emulatorStateContext.Pause();
    }
}