using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private void SetupScfInstruction()
    {
        _instructions[Opcode.Scf] = new ALUInstruction(WriteType.None, ALUOpType.Scf, FetchType.None);
    }

    private (ushort, ushort) Scf(ushort _)
    {
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        
        SetFlag(Flag.Carry);

        return (_, 4);
    }
}