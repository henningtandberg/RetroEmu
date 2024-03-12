using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _instructions[Opcode.JpNZ_N16] = new JumpInstruction(WriteType.PC, ConditionalOpType.JpConditionally, FetchType.N16, processor => !processor.IsSet(Flag.Zero));
            _instructions[Opcode.Jp_N16] = new Instruction(WriteType.PC, OpType.Jp, FetchType.N16);
            _instructions[Opcode.JpZ_N16] = new JumpInstruction(WriteType.PC, ConditionalOpType.JpConditionally, FetchType.N16, processor => processor.IsSet(Flag.Zero));
            _instructions[Opcode.JpNC_N16] = new JumpInstruction(WriteType.PC, ConditionalOpType.JpConditionally, FetchType.N16, processor => !processor.IsSet(Flag.Carry));
            _instructions[Opcode.JpC_N16] = new JumpInstruction(WriteType.PC, ConditionalOpType.JpConditionally, FetchType.N16, processor => processor.IsSet(Flag.Carry));
            _instructions[Opcode.Jp_XHL] = new Instruction(WriteType.PC, OpType.Jp, FetchType.XHL);
        }
        
        private (ushort, ushort) JumpConditionally(ushort input, bool condition)
        {
            return condition
                ? new (input, 4)
                : new (*Registers.PC, 0);
        }
        
        private static (ushort, ushort) Jump(ushort input)
        {
            return (input, 4);
        }
    }
}
