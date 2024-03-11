using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

internal record JumpInstruction(WriteType WriteOp, OpType Op, FetchType FetchOp, Func<Processor, bool> Condition) : IInstruction
{
    public unsafe int Execute(Processor processor,
        delegate*<Processor, IOperationInput, IOperationOutput>[] ops, delegate*<Processor, ushort, byte>[] writeOps)
    {
        var condition = Condition(processor);

        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var operationOutput = ops[(int)Op](processor, new JumpOperationInput(fetchResult, condition));
        var writeCycles = writeOps[(int)WriteOp](processor, operationOutput.Value);

        return fetchCycles + operationOutput.Cycles + writeCycles;
    }
}