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
        
        private OperationOutput JumpConditionally(IOperationInput jumpOperationInput)
        {
            JumpOperationInput input = (JumpOperationInput)jumpOperationInput;
            return input.ConditionIsMet
                ? new OperationOutput(input.Value, 4)
                : new OperationOutput(*Registers.PC, 0);
        }
        
        private static OperationOutput Jump(IOperationInput jumpOperationInput)
        {
            var newPc = jumpOperationInput.Value;
            return new OperationOutput(newPc, 4);
        }
    }
}
