namespace RetroEmu.Devices.DMG.CPU.Instructions;
internal record RestartInstruction(byte nextAddress) : IInstruction
{
    public int Execute(Processor processor)
    {
        var (newPc, cycles) = processor.RestartAtGivenAddress(nextAddress);
        var writeCycles = processor.PerformWriteOperation(WriteType.PC, newPc);

        return cycles + writeCycles;
    }
}
