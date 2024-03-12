namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record Instruction(WriteType WriteOp, OpType Op, FetchType FetchOp) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var operationOutput = processor.PerformOpOperation(Op, new OperationInput(fetchResult));
        var writeCycles = processor.PerformWriteOperation(WriteOp, operationOutput.Value);

        return fetchCycles + operationOutput.Cycles + writeCycles;
    }
}
