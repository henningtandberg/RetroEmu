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
            
            Push16ToStack(nextInstruction);

            return new(input, 8);
        }
    }
}