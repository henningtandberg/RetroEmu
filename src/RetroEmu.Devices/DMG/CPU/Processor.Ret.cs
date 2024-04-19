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
            
            // Not sure if this is the correct byte order YOLO
            ushort nextInstruction = _memory.Read((ushort)(*Registers.SP + 2));
            nextInstruction <<= 8;
            nextInstruction |= _memory.Read((ushort)(*Registers.SP + 1));
            *Registers.SP += 2;

            return new(nextInstruction, 8);
        }
    }
}