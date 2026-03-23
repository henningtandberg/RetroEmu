using System;
using RetroEmu.Devices;
using RetroEmu.Devices.GameBoy;

namespace RetroEmu.Runtime.State;

internal sealed class LoadState(IEmulatorStateContext emulatorStateContext, byte[] cartridgeData)
    : BaseEmulatorState(emulatorStateContext)
{
    public override void Update(IFrameCounter _, IGameBoy gameBoy)
    {
        Console.WriteLine("Loading cartridge");
        gameBoy.Reset();
        gameBoy.Load(cartridgeData);
        _emulatorStateContext.Start();
    }
}