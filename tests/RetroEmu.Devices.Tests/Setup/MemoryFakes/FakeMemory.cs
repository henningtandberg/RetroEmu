using System.Collections.Generic;
using RetroEmu.Devices.DMG;

namespace RetroEmu.Devices.Tests.Setup.MemoryFakes;

public class FakeMemory(IDictionary<ushort, byte> memory) : IMemory
{
    
    public void Reset()
    {
        // Nothing to do
    }

    public byte Read(ushort address)
    {
        return memory.TryGetValue(address, out var data)
            ? data
            : throw new KeyNotFoundException($"Address {address} not found in memory");
    }

    public void Write(ushort address, byte value)
    {
        memory[address] = value;
    }
}