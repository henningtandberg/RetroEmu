namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record Instruction(WriteType WriteOp, OpType Op, FetchType FetchOp) : IInstruction
{
    public unsafe int Execute(Processor processor, delegate*<Processor, IOperationInput, IOperationOutput>[] ops)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var operationOutput = ops[(int)Op](processor, new OperationInput(fetchResult));
        var writeCycles = processor.PerformWriteOperation(WriteOp, operationOutput.Value);

        return fetchCycles + operationOutput.Cycles + writeCycles;
    }
}
