using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private static CBType DecodeCbType(byte cbOpcode) =>
        (CBType)(cbOpcode >> 3);

    private static FetchType DecodeFetchType(byte cbOpcode) => (cbOpcode & 0x07) switch
    {
        0 => FetchType.B,
        1 => FetchType.C,
        2 => FetchType.D,
        3 => FetchType.E,
        4 => FetchType.H,
        5 => FetchType.L,
        6 => FetchType.XHL,
        7 => FetchType.A,
        _ => throw new NotSupportedException()
    };

    private static WriteType DecodeWriteType(byte cbOpcode) => (cbOpcode & 0x07) switch
    {
        0 => WriteType.B,
        1 => WriteType.C,
        2 => WriteType.D,
        3 => WriteType.E,
        4 => WriteType.H,
        5 => WriteType.L,
        6 => WriteType.XHL,
        7 => WriteType.A,
        _ => throw new NotSupportedException()
    };

    private (ushort, ushort) PerformCbOperation(CBType cbType, ushort fetchValue) => cbType switch
    {
        CBType.RLC => RotateLeft((byte)fetchValue),
        CBType.RRC => RotateRight((byte)fetchValue),
        CBType.RL => RotateLeftThroughCarry((byte)fetchValue),
        CBType.RR => RotateRightThroughCarry((byte)fetchValue),
        CBType.SWAP => Swap((byte)fetchValue),
        CBType.BIT0 => Bit(fetchValue, 0),
        CBType.BIT1 => Bit(fetchValue, 1),
        CBType.BIT2 => Bit(fetchValue, 2),
        CBType.BIT3 => Bit(fetchValue, 3),
        CBType.BIT4 => Bit(fetchValue, 4),
        CBType.BIT5 => Bit(fetchValue, 5),
        CBType.BIT6 => Bit(fetchValue, 6),
        CBType.BIT7 => Bit(fetchValue, 7),
        CBType.RES0 => Res(fetchValue, 0),
        CBType.RES1 => Res(fetchValue, 1),
        CBType.RES2 => Res(fetchValue, 2),
        CBType.RES3 => Res(fetchValue, 3),
        CBType.RES4 => Res(fetchValue, 4),
        CBType.RES5 => Res(fetchValue, 5),
        CBType.RES6 => Res(fetchValue, 6),
        CBType.RES7 => Res(fetchValue, 7),
        CBType.SET0 => Set(fetchValue, 0),
        CBType.SET1 => Set(fetchValue, 1),
        CBType.SET2 => Set(fetchValue, 2),
        CBType.SET3 => Set(fetchValue, 3),
        CBType.SET4 => Set(fetchValue, 4),
        CBType.SET5 => Set(fetchValue, 5),
        CBType.SET6 => Set(fetchValue, 6),
        CBType.SET7 => Set(fetchValue, 7),
        CBType.SLA => ShiftLeftA(fetchValue),
        CBType.SRA => ShiftRightA(fetchValue),
        CBType.SRL => ShiftRightL(fetchValue),
        _ => throw new NotImplementedException()
    };

    private (ushort, ushort) Bit(ushort fetchValue, byte bit)
    {
        var b = (byte)((fetchValue >> bit) & 0x01);

        SetFlagToValue(Flag.Zero, b == 0);
        ClearFlag(Flag.Subtract);
        SetFlag(Flag.HalfCarry);

        return (fetchValue, 4);
    }

    private static (ushort, ushort) Res(ushort fetchValue, byte bit)
    {
        var b = (byte)(fetchValue & ~(0x01 << bit));

        return (b, 4);
    }

    private static (ushort, ushort) Set(ushort fetchValue, byte bit)
    {
        var b = (byte)(fetchValue | (0x01 << bit));

        return (b, 4);
    }

    private (ushort, ushort) Swap(byte fetchValue)
    {
        var upper = fetchValue & 0xf0;
        var lower = fetchValue & 0x0f;
        var swapped = (upper >> 4) | (lower << 4);

        SetFlagToValue(Flag.Zero, swapped == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.Carry);
        ClearFlag(Flag.HalfCarry);

        return ((ushort)swapped, 4);
    }

    private (ushort, ushort) ShiftLeftA(ushort input)
    {
        var newCarry = (input & 0x80) > 0;

        var result = (byte)(input << 1);

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return ((ushort)result, 4);
    }

    private (ushort, ushort) ShiftRightA(ushort input)
    {
        var newCarry = (input & 0x01) > 0;
        var msb = input & 0x80;

        var result = (input >> 1) | msb;

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return ((ushort)result, 4);
    }

    private (ushort, ushort) ShiftRightL(ushort input)
    {
        var newCarry = (input & 0x01) > 0;

        var result = (input >> 1);

        SetFlagToValue(Flag.Zero, result == 0);
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        SetFlagToValue(Flag.Carry, newCarry);

        return ((ushort)result, 4);
    }
}
