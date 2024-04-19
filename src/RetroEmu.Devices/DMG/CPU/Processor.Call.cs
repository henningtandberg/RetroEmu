using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupCallInstructions()
        {
            _instructions[Opcode.Call_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.Always);
            _instructions[Opcode.CallNZ_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.NZ);
            _instructions[Opcode.CallZ_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.Z);
            _instructions[Opcode.CallNC_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.NC);
            _instructions[Opcode.CallC_N16] = new ConditionalInstruction(WriteType.PC, ConditionalOpType.Call, FetchType.N16, ConditionType.C);
        }

        private (ushort, ushort) CallConditionally(ushort input, bool condition)
        {
            var nextInstruction = *Registers.PC;

            if (!condition)
                return new(*Registers.PC, 4);
            
            // Not sure if this is the correct byte order YOLO
            _memory.Write((ushort)(*Registers.SP-0), (byte)(nextInstruction >> 8));
            _memory.Write((ushort)(*Registers.SP-1), (byte)nextInstruction);
            *Registers.SP -= 2;

            return new(input, 8);
        }
    }
}