using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupRetInstructions()
        {
            _instructions[Opcode.Ret] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.Always);
            _instructions[Opcode.RetNZ] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.NZ);
            _instructions[Opcode.RetZ] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.Z);
            _instructions[Opcode.RetNC] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.NC);
            _instructions[Opcode.RetC] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Ret, FetchType.SP, ConditionType.C);
        }

        private (ushort, ushort) ReturnConditionally(ushort input, bool condition)
        {
            if (!condition)
                return new(*Registers.PC, 4);
            
            var (_, nextInstruction) = Pop16FromStack();

            return new(nextInstruction, 8);
        }
    }
}