namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupJpInstructions()
        {
            _ops[(int)OpType.JpNZ] = &JumpNZ;
            _ops[(int)OpType.Jp] = &Jump;
            _ops[(int)OpType.JpZ] = &JumpZ;
            _ops[(int)OpType.JpNC] = &JumpNC;
            _ops[(int)OpType.JpC] = &JumpC;

            // TODO: More compact way of writing Jumps?
            //_instructions[Opcode.JpNZ_N16] = new ConditionalJumpInstruction(WriteType.PC, OpType.JpConditionally, FetchType.N16, processor => !processor.IsSet(Flag.Zero));
            _instructions[Opcode.JpNZ_N16] = new Instruction(WriteType.PC, OpType.JpNZ, FetchType.N16);
            _instructions[Opcode.Jp_N16] = new Instruction(WriteType.PC, OpType.Jp, FetchType.N16);
            _instructions[Opcode.JpZ_N16] = new Instruction(WriteType.PC, OpType.JpZ, FetchType.N16);
            _instructions[Opcode.JpNC_N16] = new Instruction(WriteType.PC, OpType.JpNC, FetchType.N16);
            _instructions[Opcode.JpC_N16] = new Instruction(WriteType.PC, OpType.JpC, FetchType.N16);
            _instructions[Opcode.Jp_XHL] = new Instruction(WriteType.PC, OpType.JpC, FetchType.XHL);
        }

        //private static (byte, ushort) JumpConditionally(IProcessor processor, ushort value, bool conditionIsMet)
        //{
        //    return conditionIsMet
        //        ? ((byte)4, value)
        //        : ((byte)4, *processor.Registers.PC);
        //}

        private static (byte, ushort) JumpNZ(Processor processor, ushort value)
        {
            return !processor.IsSet(Flag.Zero)
                ? ((byte)4, value)
                : ((byte)0, *processor.Registers.PC);
        }
        
        private static (byte, ushort) JumpZ(Processor processor, ushort value)
        {
            return processor.IsSet(Flag.Zero)
                ? ((byte)4, value)
                : ((byte)0, *processor.Registers.PC);
        }
        
        private static (byte, ushort) JumpNC(Processor processor, ushort value)
        {
            return !processor.IsSet(Flag.Carry)
                ? ((byte)4, value)
                : ((byte)0, *processor.Registers.PC);
        }
        
        private static (byte, ushort) JumpC(Processor processor, ushort value)
        {
            return processor.IsSet(Flag.Carry)
                ? ((byte)4, value)
                : ((byte)0, *processor.Registers.PC);
        }
        
        private static (byte, ushort) Jump(Processor _, ushort newPc)
        {
            return (4, newPc);
        }
    }
}
