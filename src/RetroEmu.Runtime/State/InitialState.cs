using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

/// <summary>
/// This is a special state which no other state will transition to
/// and it will only transition to LoadState itself.
/// </summary>
internal sealed class InitialState(IEmulatorStateContext emulatorStateContext) : IEmulatorState
{
    public void HandleStart()
    { }

    public void HandlePause()
    { }

    public void HandleLoad(byte[] cartridgeData) =>
        emulatorStateContext.SetState(new LoadState(emulatorStateContext, cartridgeData));

    public void HandleStep()
    { }

    public void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    { }
}