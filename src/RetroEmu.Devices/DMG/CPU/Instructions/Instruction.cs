namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record Instruction(WriteType WriteOp, OpType Op, FetchType FetchOp) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var (opResult, opCycles) = processor.PerformOpOperation(Op, fetchResult);
        var writeCycles = processor.PerformWriteOperation(WriteOp, opResult);

        return fetchCycles + opCycles + writeCycles;
    }
}
