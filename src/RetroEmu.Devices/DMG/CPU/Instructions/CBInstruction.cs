using System;
using System.IO;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record CBInstruction() : IInstruction
{
    private CBType DecodeCBType(byte cbOpcode)
    {
        return (CBType)(cbOpcode >> 3);
    }
    private FetchType DecodeFetchType(byte cbOpcode)
    {
        return (cbOpcode & 0x07) switch {
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
    }

    private WriteType DecodeWriteType(byte cbOpcode)
    {
        return (cbOpcode & 0x07) switch
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

    public unsafe int Execute(Processor processor)
    {
        var cbOpCode = processor.GetNextOpcode();
        var cbType = DecodeCBType(cbOpCode);
        var fetchType = DecodeFetchType(cbOpCode);
        var writeType = DecodeWriteType(cbOpCode);
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(fetchType);
        var (opCycles, opResult) = processor.PerformCBOperation(cbType, fetchResult);
        var writeCycles = processor.PerformWriteOperation(writeType, opResult);

        var cbCycles = 4;
        return cbCycles + fetchCycles + opCycles + writeCycles;
    }
}