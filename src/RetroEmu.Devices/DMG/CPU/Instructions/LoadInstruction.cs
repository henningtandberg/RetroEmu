using System;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record LoadInstruction(WriteType WriteOp, FetchType FetchOp) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var operationResult = new OperationOutput(fetchResult, 4);
        var writeCycles = processor.PerformWriteOperation(WriteOp, operationResult.Value);

        return fetchCycles + operationResult.Cycles + writeCycles;
    }
}