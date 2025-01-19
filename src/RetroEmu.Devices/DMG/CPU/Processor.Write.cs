using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor
{
    private byte PerformWriteOperation(WriteType writeType, ushort value) => writeType switch
    {
        WriteType.A => WriteValue(ref Registers.A, (byte)value),
        WriteType.B => WriteValue(ref Registers.B, (byte)value),
        WriteType.C => WriteValue(ref Registers.C, (byte)value),
        WriteType.D => WriteValue(ref Registers.D, (byte)value),
        WriteType.E => WriteValue(ref Registers.E, (byte)value),
        WriteType.H => WriteValue(ref Registers.H, (byte)value),
        WriteType.L => WriteValue(ref Registers.L, (byte)value),
        WriteType.XBC => WriteAtAddress(Registers.BC, (byte)value),
        WriteType.XDE => WriteAtAddress(Registers.DE, (byte)value),
        WriteType.XHL => WriteAtAddress(Registers.HL, (byte)value),
        WriteType.XHLD => WriteAtAddress(Registers.HL--, (byte)value),
        WriteType.XHLI => WriteAtAddress(Registers.HL++, (byte)value),
        WriteType.XN16 => WriteAtImmediateAddress((byte)value),
        WriteType.XC => WriteAtAddress_RegC_0xFF00((byte)value),
        WriteType.XN8 => WriteAtImmediateAddress_Immediate_0xFF00((byte)value),
        WriteType.AF => WriteValue16AF(value),
        WriteType.BC => WriteValue16(ref Registers.BC, value),
        WriteType.DE => WriteValue16(ref Registers.DE, value),
        WriteType.HL => WriteValue16(ref Registers.HL, value),
        WriteType.SP => WriteValue16(ref Registers.SP, value),
        WriteType.PC => WriteValue16(ref Registers.PC, value),
        WriteType.Push => Push16ToStack(value),
        WriteType.None => 0,
        _ => throw new NotImplementedException()
    };

    private byte Push16ToStack(ushort value)
    {
        // Not sure if this is the correct byte order YOLO
        memory.Write((ushort)(Registers.SP - 1), (byte)(value >> 8));
        memory.Write((ushort)(Registers.SP - 2), (byte)value);
        Registers.SP -= 2;
       
        return 16;
    }

    private static byte WriteValue(ref byte dst, byte value)
    {
        dst = value;
        return 0;
    }

    private byte WriteAtAddress(ushort address, byte value)
    {
        memory.Write(address, value);
        return 4;
    }

    private byte WriteAtImmediateAddress(byte value)
    {
        var addressLsb = GetNextOpcode();
        var addressMsb = GetNextOpcode();
        var address = (ushort)((addressMsb << 8) | addressLsb);
        memory.Write(address, value);
        return 12;
    }

    private byte WriteAtImmediateAddress_Immediate_0xFF00(byte value)
    {
        var im = GetNextOpcode();
        var address = im + 0xFF00;
        memory.Write((ushort)address, value);
        return 8;
    }

    private byte WriteAtAddress_RegC_0xFF00(byte value)
    {
        return WriteAtAddress((ushort)(0xFF00 + Registers.C), value);
    }
    private byte WriteValue16AF(ushort value)
    {
        Registers.AF = value;
        return 0;
    }
    private static byte WriteValue16(ref ushort dst, ushort value)
    {
        dst = value;
        return 0;
    }
}