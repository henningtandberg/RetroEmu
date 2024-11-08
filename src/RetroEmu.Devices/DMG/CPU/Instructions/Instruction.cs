namespace RetroEmu.Devices.DMG.CPU.Instructions;

internal record Instruction(WriteType WriteType, OpType OpType, FetchType FetchType) : IInstruction
{
    
    // This will be removed later together with the IInstruction interface
    public int Execute(Processor processor)
    {
        throw new System.NotImplementedException("Should not have ended up here!");
    }
}