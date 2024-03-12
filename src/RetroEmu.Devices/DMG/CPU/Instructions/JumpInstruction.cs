using System;

namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record JumpInstruction(WriteType WriteOp, OpType Op, FetchType FetchOp, Func<Processor, bool> Condition) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var condition = Condition(processor);

        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var operationOutput = processor.PerformOpOperation(Op, new JumpOperationInput(fetchResult, condition));
        var writeCycles = processor.PerformWriteOperation(WriteOp, operationOutput.Value);

        return fetchCycles + operationOutput.Cycles + writeCycles;
    }
}