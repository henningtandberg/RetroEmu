using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.GB.TestSetup.MemoryFakes;

public class FakeMemory : Memory
{
    public FakeMemory(
        ITimer timer,
        IPixelProcessingUnit pixelProcessingUnit,
        IInterruptState interruptState,
        IJoypad joypad,
        ICartridge cartridge,
        IDictionary<ushort, byte> initialMemory) :
        base(timer,
            pixelProcessingUnit,
            interruptState, 
            joypad,
            cartridge)
    {
        foreach (var (address, value) in initialMemory)
        {
            if (address <= 0x7FFF)
            {
                cartridge.WriteForTests(address, value);
            }
            else
            {
                Write(address, value);
            }
        }
    }
}