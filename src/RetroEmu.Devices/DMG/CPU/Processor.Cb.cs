using System;
using System.Diagnostics;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        internal (byte, ushort) PerformCBOperation(CBType cbType, ushort fetchValue)
        {
            return cbType switch {
                _ => throw new NotImplementedException()
            };
        }
    }
}
