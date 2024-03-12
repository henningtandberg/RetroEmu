using System;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record ConditionalInstruction(WriteType WriteOp, ConditionalOpType Op, FetchType FetchOp, ConditionType Condition) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var condition = processor.PerformConditionOperation(Condition);

        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var (opResult, opCycles) = processor.PerformConditionalOpOperation(Op, fetchResult, condition);
        var writeCycles = processor.PerformWriteOperation(WriteOp, opResult);

        return fetchCycles + opCycles + writeCycles;
    }
}