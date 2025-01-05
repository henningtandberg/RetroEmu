using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.Tests.Setup.MemoryFakes;

public class FakeMemory : Memory
{
    public FakeMemory(ITimer timer, IDictionary<ushort, byte> initialMemory) : base(timer)
    {
        foreach (var (address, value) in initialMemory)
        {
            Write(address, value);
        }
    }
}