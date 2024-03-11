namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record Instruction(WriteType WriteOp, OpType Op, FetchType FetchOp) : IInstruction
{
    public unsafe int Execute(Processor processor,
        delegate*<Processor, IOperationInput, IOperationOutput>[] ops, delegate*<Processor, ushort, byte>[] writeOps)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var operationOutput = ops[(int)Op](processor, new OperationInput(fetchResult));
        var writeCycles = writeOps[(int)WriteOp](processor, operationOutput.Value);

        return fetchCycles + operationOutput.Cycles + writeCycles;
    }
}
