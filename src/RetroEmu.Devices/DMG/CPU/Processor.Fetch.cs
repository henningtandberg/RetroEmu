using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private (byte, ushort) PerformFetchOperation(FetchType fetchType) => fetchType switch
    {
        FetchType.A => FetchValue(Registers.A),
        FetchType.B => FetchValue(Registers.B),
        FetchType.C => FetchValue(Registers.C),
        FetchType.D => FetchValue(Registers.D),
        FetchType.E => FetchValue(Registers.E),
        FetchType.H => FetchValue(Registers.H),
        FetchType.L => FetchValue(Registers.L),
        FetchType.XBC => FetchFromAddress(Registers.BC),
        FetchType.XDE => FetchFromAddress(Registers.DE),
        FetchType.XHL => FetchFromAddress(Registers.HL),
        FetchType.XHLD => FetchFromAddress(Registers.HL--),
        FetchType.XHLI => FetchFromAddress(Registers.HL++),
        FetchType.XN16 => FetchFromImmediateAddress(),
        FetchType.N8 => FetchFromImmediateValue(),
        FetchType.XN8 => FetchFromAddress_Immediate_0xFF00(),
        FetchType.XC => FetchFromAddress_RegC_0xFF00(),
        FetchType.SPN8 => FetchFromAddress_SP_N8(),
        FetchType.AF => FetchValue16(Registers.AF),
        FetchType.BC => FetchValue16(Registers.BC),
        FetchType.DE => FetchValue16(Registers.DE),
        FetchType.HL => FetchValue16(Registers.HL),
        FetchType.PC => FetchValue16(Registers.PC),
        FetchType.SP => FetchValue16(Registers.SP),
        FetchType.N16 => FetchImmediateValue16(),
        FetchType.Pop => Pop16FromStack(),
        FetchType.Address00H => (0, 0x00),
        FetchType.Address08H => (0, 0x08),
        FetchType.Address10H => (0, 0x10),
        FetchType.Address18H => (0, 0x18),
        FetchType.Address20H => (0, 0x20),
        FetchType.Address28H => (0, 0x28),
        FetchType.Address30H => (0, 0x30),
        FetchType.Address38H => (0, 0x38),
        FetchType.None => (0, 0),
        _ => throw new NotImplementedException()
    };

    private (byte, ushort) Pop16FromStack()
    {
        // Not sure if this is the correct byte order YOLO
        ushort value = memory.Read((ushort)(Registers.SP + 1));
        value <<= 8;
        value |= memory.Read((ushort)(Registers.SP + 0));
        Registers.SP += 2;

        return (12, value);
    }

    private static (byte, ushort) FetchValue(byte value)
    {
        return (0, value);
    }

    private (byte, ushort) FetchFromImmediateValue()
    {
        var value = GetNextOpcode();
        return (4, value);
    }

    private (byte, ushort) FetchFromAddress(ushort address)
    {
        var value = memory.Read(address);
        return (4, value);
    }

    private (byte, ushort) FetchFromImmediateAddress()
    {
        var addressLsb = GetNextOpcode();
        var addressMsb = GetNextOpcode();
        var address = (ushort)((addressMsb << 8) | addressLsb);
        var value = memory.Read(address);
        return (12, value);
    }

    private (byte, ushort) FetchFromAddress_Immediate_0xFF00()
    {
        var im = GetNextOpcode();
        var address = 0xFF00 + im;
        var value = memory.Read((ushort)address);
        return (8, value);
    }

    private (byte, ushort) FetchFromAddress_RegC_0xFF00()
    {
        var address = 0xFF00 + Registers.C;
        var value = memory.Read((ushort)address);
        return (8, value);
    }

    private (byte, ushort) FetchFromAddress_SP_N8()
    {
        var immediate = GetNextOpcode();
        var value = Registers.SP + (sbyte)immediate;

        ClearFlag(Flag.Zero);
        ClearFlag(Flag.Subtract);

        if (value > 0xFFFF)
        {
            SetFlag(Flag.Carry);
        }
        else
        {
            ClearFlag(Flag.Carry);
        }

        if (value > 0x0FFF)
        {
            SetFlag(Flag.HalfCarry);
        }
        else
        {
            ClearFlag(Flag.HalfCarry);
        }

        return (8, (ushort)value);
    }

    private static (byte, ushort) FetchValue16(ushort value)
    {
        return (0, value);
    }

    private (byte, ushort) FetchImmediateValue16()
    {
        var addressLsb = GetNextOpcode();
        var addressMsb = GetNextOpcode();
        var value = (ushort)((addressMsb << 8) | addressLsb);
        return (8, value);
    }
}