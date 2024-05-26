using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private void SetupCbInstruction()
    {
        _instructions[Opcode.Pre_CB] = new CBInstruction();
    }

    internal (byte, ushort) PerformCBOperation(CBType cbType, ushort fetchValue)
    {
        return cbType switch {
            CBType.BIT0 => Bit(fetchValue, 0),
            CBType.BIT1 => Bit(fetchValue, 1),
            CBType.BIT2 => Bit(fetchValue, 2),
            CBType.BIT3 => Bit(fetchValue, 3),
            CBType.BIT4 => Bit(fetchValue, 4),
            CBType.BIT5 => Bit(fetchValue, 5),
            CBType.BIT6 => Bit(fetchValue, 6),
            CBType.BIT7 => Bit(fetchValue, 7),
            // Place RES here
            CBType.SET0 => Set(fetchValue, 0),
            CBType.SET1 => Set(fetchValue, 1),
            CBType.SET2 => Set(fetchValue, 2),
            CBType.SET3 => Set(fetchValue, 3),
            CBType.SET4 => Set(fetchValue, 4),
            CBType.SET5 => Set(fetchValue, 5),
            CBType.SET6 => Set(fetchValue, 6),
            CBType.SET7 => Set(fetchValue, 7),
            _ => throw new NotImplementedException()
        };
    }

    private static (byte, ushort) Set(ushort fetchValue, byte bit)
    {
        var b = (byte)(fetchValue | (0x01 << bit));

        return (4, b);
    }
    
    private (byte, ushort) Bit(ushort fetchValue, byte bit)
    {
        var b = (byte)((fetchValue >> bit) & 0x01);

        if (b == 0)
        {
            SetFlag(Flag.Zero);
        }
        else
        {
            ClearFlag(Flag.Zero);
        }
        ClearFlag(Flag.Subtract);
        SetFlag(Flag.HalfCarry);

        return (4, fetchValue);
    }
}
