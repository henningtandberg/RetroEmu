using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.Devices.Tests.Setup.MemoryFakes;

public class FakeMemory : Memory
{
    public FakeMemory(ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState, IDictionary<ushort, byte> initialMemory) : base(timer, pixelProcessingUnit, interruptState)
    {
        foreach (var (address, value) in initialMemory)
        {
            Write(address, value);
        }
    }
}