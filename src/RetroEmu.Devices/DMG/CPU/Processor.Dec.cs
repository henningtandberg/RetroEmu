using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupDecInstructions()
        {
            _ops[(int)OpType.Dec] = &Dec;

            _instructions[Opcode.Dec_A] = new GeneralInstruction(WriteType.A, OpType.Dec, FetchType.A);
            _instructions[Opcode.Dec_B] = new GeneralInstruction(WriteType.B, OpType.Dec, FetchType.B);
            _instructions[Opcode.Dec_C] = new GeneralInstruction(WriteType.C, OpType.Dec, FetchType.C);
            _instructions[Opcode.Dec_D] = new GeneralInstruction(WriteType.D, OpType.Dec, FetchType.D);
            _instructions[Opcode.Dec_E] = new GeneralInstruction(WriteType.E, OpType.Dec, FetchType.E);
            _instructions[Opcode.Dec_H] = new GeneralInstruction(WriteType.H, OpType.Dec, FetchType.H);
            _instructions[Opcode.Dec_L] = new GeneralInstruction(WriteType.L, OpType.Dec, FetchType.L);
            _instructions[Opcode.Dec_XHL] = new GeneralInstruction(WriteType.XHL, OpType.Dec, FetchType.XHL);
        }

        private static (byte, ushort) Dec(Processor processor, ushort value)
        {
            var result = (int)value - 1;

            if (result > 0xFF)
            {
                processor.SetFlag(Flag.Carry);
            }
            else
            {
                processor.ClearFlag(Flag.Carry);
            }

            if (result == 0x0F)
            {
                processor.SetFlag(Flag.HalfCarry);
            }
            else
            {
                processor.ClearFlag(Flag.HalfCarry);
            }

            processor.SetFlag(Flag.Subtract);

            if (result == 0)
            {
                processor.SetFlag(Flag.Zero);
            }
            else
            {
                processor.ClearFlag(Flag.Zero);
            }

            return (4, (ushort)result); // cycles
        }
    }
}