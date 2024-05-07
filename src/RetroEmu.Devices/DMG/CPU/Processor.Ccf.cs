using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor
{
    private void SetupCcfInstruction()
    {
        _instructions[Opcode.Ccf] = new ALUInstruction(WriteType.None, ALUOpType.Ccf, FetchType.None);
    }

    private (ushort, ushort) Ccf(ushort _)
    {
        ClearFlag(Flag.Subtract);
        ClearFlag(Flag.HalfCarry);
        
        ToggleFlag(Flag.Carry);

        return (_, 4);
    }
}