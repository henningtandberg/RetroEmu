using System.Collections.Generic;
using RetroEmu.Devices.DMG;

namespace RetroEmu.GB.TestSetup;

public class AddressBusFake(IDictionary<ushort, byte> memory) : IAddressBus
{
    // TODO: Remove
    public string GetOutput() => string.Empty;

    public void Reset()
    {
        // TODO: Remove
    }

    public byte Read(ushort address) =>
        memory.TryGetValue(address, out var value)
            ? value
            : (byte)0;

    public void Write(ushort address, byte value)
    {
        if (memory.TryAdd(address, value))
        {
            return;
        }
        
        memory[address] = value;
    }

    public void Load(byte[] rom)
    {
        // TODO: Remove
    }
}