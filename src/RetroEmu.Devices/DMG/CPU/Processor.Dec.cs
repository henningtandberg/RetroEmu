using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupDecInstructions()
        {
            _instructions[Opcode.Dec_A] = new ALUInstruction(WriteType.A, ALUOpType.Dec, FetchType.A);
            _instructions[Opcode.Dec_B] = new ALUInstruction(WriteType.B, ALUOpType.Dec, FetchType.B);
            _instructions[Opcode.Dec_C] = new ALUInstruction(WriteType.C, ALUOpType.Dec, FetchType.C);
            _instructions[Opcode.Dec_D] = new ALUInstruction(WriteType.D, ALUOpType.Dec, FetchType.D);
            _instructions[Opcode.Dec_E] = new ALUInstruction(WriteType.E, ALUOpType.Dec, FetchType.E);
            _instructions[Opcode.Dec_H] = new ALUInstruction(WriteType.H, ALUOpType.Dec, FetchType.H);
            _instructions[Opcode.Dec_L] = new ALUInstruction(WriteType.L, ALUOpType.Dec, FetchType.L);
            _instructions[Opcode.Dec_XHL] = new ALUInstruction(WriteType.XHL, ALUOpType.Dec, FetchType.XHL);
        }

        private (ushort, ushort) Dec(ushort input)
        {
            var result = (int)input - 1;

            if (result > 0xFF)
            {
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

            if (result == 0x0F)
            {
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            SetFlag(Flag.Subtract);

            if (result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            return ((ushort)result, 4);
        }
    }
}