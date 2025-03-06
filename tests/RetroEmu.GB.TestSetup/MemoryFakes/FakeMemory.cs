using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.GB.TestSetup.MemoryFakes;

public class FakeMemory : Memory
{
    public FakeMemory(ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState, IJoypad joypad, IDictionary<ushort, byte> initialMemory) : base(timer, pixelProcessingUnit, interruptState, joypad)
    {
        foreach (var (address, value) in initialMemory)
        {
            if (address <= 0x7FFF)
            {
                WriteROM(address, value);
            }
            else
            {
                Write(address, value);
            }
        }
    }
}