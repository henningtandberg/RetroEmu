namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record RotationInstruction(WriteType WriteOp, RotateOpType Op, FetchType FetchOp, RotationDirection Direction) : IInstruction
{
    public int Execute(Processor processor)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var (opResult, opCycles) = processor.PerformRotateOpOperation(Op, fetchResult, Direction);
        var writeCycles = processor.PerformWriteOperation(WriteOp, opResult);

        return fetchCycles + opCycles + writeCycles;
    }
}