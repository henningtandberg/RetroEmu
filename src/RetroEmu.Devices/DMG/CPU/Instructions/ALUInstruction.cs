namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record ALUInstruction(WriteType WriteOp, ALUOpType Op, FetchType FetchOp) : IInstruction
{
    public unsafe int Execute(Processor processor)
    {
        var (fetchCycles, fetchResult) = processor.PerformFetchOperation(FetchOp);
        var (opResult, opCycles) = processor.PerformALUOpOperation(Op, fetchResult);
        var writeCycles = processor.PerformWriteOperation(WriteOp, opResult);

        return fetchCycles + opCycles + writeCycles;
    }
}
