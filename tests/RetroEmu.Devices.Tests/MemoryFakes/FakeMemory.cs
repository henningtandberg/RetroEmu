using System.Collections.Generic;
using RetroEmu.Devices.DMG;

namespace RetroEmu.Devices.Tests.MemoryFakes;

public class FakeMemory(IReadOnlyDictionary<ushort, byte> memory) : IMemory
{
    
    public void Reset()
    {
        // Nothing to do
    }

    public byte Read(ushort address)
    {
        throw new System.NotImplementedException();
    }

    public void Write(ushort address, byte value)
    {
        throw new System.NotImplementedException();
    }

    public byte Get(ushort address)
    {
        return memory.TryGetValue(address, out var data)
            ? data
            : throw new KeyNotFoundException($"Address {address} not found in memory");
    }
}