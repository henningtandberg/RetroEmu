using System;
using RetroEmu.Devices.DMG;

namespace RetroEmu.State;

internal sealed class LoadState(IApplicationStateContext applicationStateContext, byte[] cartridgeData)
    : BaseApplicationState(applicationStateContext)
{
    public override void Update(IFrameCounter _, IGameBoy gameBoy)
    {
        Console.WriteLine("Loading cartridge");
        gameBoy.Reset();
        gameBoy.Load(cartridgeData);
        _applicationStateContext.Start();
    }
}