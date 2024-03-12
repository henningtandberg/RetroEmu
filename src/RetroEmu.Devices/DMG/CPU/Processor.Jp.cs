using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _instructions[Opcode.JpNZ_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NZ);
            _instructions[Opcode.Jp_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.Always);
            _instructions[Opcode.JpZ_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NZ);
            _instructions[Opcode.JpNC_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NZ);
            _instructions[Opcode.JpC_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NZ);
            _instructions[Opcode.Jp_XHL] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.XHL, ConditionType.Always);
        }
        
        private (ushort, ushort) JumpConditionally(ushort input, bool condition)
        {
            return condition
                ? new (input, 4)
                : new (*Registers.PC, 0);
        }
    }
}
