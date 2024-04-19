using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _instructions[Opcode.Jp_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.Always);
            _instructions[Opcode.JpNZ_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NZ);
            _instructions[Opcode.JpZ_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.Z);
            _instructions[Opcode.JpNC_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.NC);
            _instructions[Opcode.JpC_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.N16, ConditionType.C);
            _instructions[Opcode.Jp_XHL] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jp, FetchType.XHL, ConditionType.Always);

            _instructions[Opcode.Jr_N8] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.Always);
            _instructions[Opcode.JrNZ_N8] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.NZ);
            _instructions[Opcode.JrZ_N8] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.Z);
            _instructions[Opcode.JrNC_N8] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.NC);
            _instructions[Opcode.JrC_N8] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Jr, FetchType.N8, ConditionType.C);
        }
        
        private (ushort, ushort) JumpConditionally(ushort input, bool condition)
        {
            return condition
                ? new (input, 8)
                : new (*Registers.PC, 4);
        }

        private (ushort, ushort) JumpRelativeConditionally(ushort input, bool condition)
        {
            var target = *Registers.PC + input;
            return JumpConditionally((ushort)target, condition);
        }
    }
}
