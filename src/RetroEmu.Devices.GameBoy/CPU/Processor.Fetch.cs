using System;
using RetroEmu.Devices.GameBoy.CPU.Instructions;

namespace RetroEmu.Devices.GameBoy.CPU;

public partial class Processor
{
    private FetchResult PerformFetchOperation(FetchType fetchType) => fetchType switch
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
        FetchType.Address00H => new FetchResult(0, 0x00),
        FetchType.Address08H => new FetchResult(0, 0x08),
        FetchType.Address10H => new FetchResult(0, 0x10),
        FetchType.Address18H => new FetchResult(0, 0x18),
        FetchType.Address20H => new FetchResult(0, 0x20),
        FetchType.Address28H => new FetchResult(0, 0x28),
        FetchType.Address30H => new FetchResult(0, 0x30),
        FetchType.Address38H => new FetchResult(0, 0x38),
        FetchType.None => new FetchResult(0, 0),
        _ => throw new NotImplementedException()
    };

    private FetchResult Pop16FromStack()
    {
        // Not sure if this is the correct byte order YOLO
        ushort value = addressBus.Read((ushort)(Registers.SP + 1));
        value <<= 8;
        value |= addressBus.Read((ushort)(Registers.SP + 0));
        Registers.SP += 2;

        return new FetchResult(12, value);
    }

    private static FetchResult FetchValue(byte value)
    {
        return new FetchResult(0, value);
    }

    private FetchResult FetchFromImmediateValue()
    {
        var value = GetNextOpcode();
        return new FetchResult(4, value);
    }

    private FetchResult FetchFromAddress(ushort address)
    {
        var value = addressBus.Read(address);
        return new FetchResult(4, value);
    }

    private FetchResult FetchFromImmediateAddress()
    {
        var addressLsb = GetNextOpcode();
        var addressMsb = GetNextOpcode();
        var address = (ushort)((addressMsb << 8) | addressLsb);
        var value = addressBus.Read(address);
        return new FetchResult(12, value);
    }

    private FetchResult FetchFromAddress_Immediate_0xFF00()
    {
        var im = GetNextOpcode();
        var address = 0xFF00 + im;
        var value = addressBus.Read((ushort)address);
        return new FetchResult(8, value);
    }

    private FetchResult FetchFromAddress_RegC_0xFF00()
    {
        var address = 0xFF00 + Registers.C;
        var value = addressBus.Read((ushort)address);
        return new FetchResult(8, value);
    }

    private FetchResult FetchFromAddress_SP_N8()
    {
        var immediate = GetNextOpcode();
        var registerSP = Registers.SP;
        var result1 = (0x000F & registerSP) + (0x000F & (byte)immediate);
        var result2 = (0x00FF & registerSP) + (0x00FF & (byte)immediate);
        var halfCarry = result1 > 0x0F;
        var carry = result2 > 0xFF;

        var resultLSB = (byte)result2;
        var resultMSB = registerSP & 0xFF00;

        var isNegative = (sbyte)immediate < 0;
        if (!isNegative && carry)
        {
            resultMSB += 0x0100;
        }
        else if (isNegative && !carry)
        {
            resultMSB -= 0x0100;
        }
        var result = resultMSB | resultLSB;

        SetFlagToValue(Flag.Carry, carry); // Set or reset according to operation?
        SetFlagToValue(Flag.HalfCarry, halfCarry); // Set or reset according to operation?
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.Zero);

        return new FetchResult(8, (ushort)result);
    }

    private static FetchResult FetchValue16(ushort value)
    {
        return new FetchResult(0, value);
    }

    private FetchResult FetchImmediateValue16()
    {
        var addressLsb = GetNextOpcode();
        var addressMsb = GetNextOpcode();
        var value = (ushort)((addressMsb << 8) | addressLsb);
        return new FetchResult(8, value);
    }
}