using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

/// <summary>
/// This is a special state which no other state will transition to
/// and it will only transition to LoadState itself.
/// </summary>
internal sealed class InitialState(IApplicationStateContext applicationStateContext) : IApplicationState
{
    public void HandleStart()
    { }

    public void HandlePause()
    { }

    public void HandleLoad(byte[] cartridgeData) =>
        applicationStateContext.SetState(new LoadState(applicationStateContext, cartridgeData));

    public void HandleStep()
    { }

    public void Update(IFrameCounter frameCounter, IGameBoy gameBoy)
    { }
}