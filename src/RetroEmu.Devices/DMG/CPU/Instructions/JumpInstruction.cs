using System;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record JumpInstruction(WriteType WriteOp, ConditionalOpType Op, FetchType FetchOp, Func<Processor, bool> Condition) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var condition = Condition(processor);

        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var (opResult, opCycles) = processor.PerformConditionalOpOperation(Op, fetchResult, condition);
        var writeCycles = processor.PerformWriteOperation(WriteOp, opResult);

        return fetchCycles + opCycles + writeCycles;
    }
}