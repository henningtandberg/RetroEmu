using System;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal static class CbInstructionDecoder
{
    public static CBType DecodeCbType(this byte cbOpcode) =>
        (CBType)(cbOpcode >> 3);

    public static FetchType DecodeFetchType(this byte cbOpcode) => (cbOpcode & 0x07) switch
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

    public static WriteType DecodeWriteType(this byte cbOpcode) => (cbOpcode & 0x07) switch
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
}