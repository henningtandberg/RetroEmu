using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _ops[(int)OpType.JpConditionally] = &JumpConditionally;
            _ops[(int)OpType.Jp] = &Jump;

            _instructions[Opcode.JpNZ_N16] = new JumpInstruction(WriteType.PC, OpType.JpConditionally, FetchType.N16, processor => !processor.IsSet(Flag.Zero));
            _instructions[Opcode.Jp_N16] = new Instruction(WriteType.PC, OpType.Jp, FetchType.N16);
            _instructions[Opcode.JpZ_N16] = new JumpInstruction(WriteType.PC, OpType.JpConditionally, FetchType.N16, processor => processor.IsSet(Flag.Zero));
            _instructions[Opcode.JpNC_N16] = new JumpInstruction(WriteType.PC, OpType.JpConditionally, FetchType.N16, processor => !processor.IsSet(Flag.Carry));
            _instructions[Opcode.JpC_N16] = new JumpInstruction(WriteType.PC, OpType.JpConditionally, FetchType.N16, processor => processor.IsSet(Flag.Carry));
            _instructions[Opcode.Jp_XHL] = new Instruction(WriteType.PC, OpType.Jp, FetchType.XHL);
        }
        
        private static OperationOutput JumpConditionally(Processor processor, IOperationInput operationInput) => 
            processor.JumpConditionally(operationInput as JumpOperationInput);
        private static OperationOutput Jump(Processor processor, IOperationInput operationInput) => Jump(operationInput);
        
        private OperationOutput JumpConditionally(JumpOperationInput jumpOperationInput)
        {
            return jumpOperationInput.ConditionIsMet
                ? new OperationOutput(jumpOperationInput.Value, 4)
                : new OperationOutput(*Registers.PC, 0);
        }
        
        private static OperationOutput Jump(IOperationInput jumpOperationInput)
        {
            var newPc = jumpOperationInput.Value;
            return new OperationOutput(newPc, 4);
        }
    }
}
