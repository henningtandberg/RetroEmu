using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        private void SetupIncInstructions()
        {
            _instructions[Opcode.Inc_A] = new ALUInstruction(WriteType.A, ALUOpType.Inc, FetchType.A);
            _instructions[Opcode.Inc_B] = new ALUInstruction(WriteType.B, ALUOpType.Inc, FetchType.B);
            _instructions[Opcode.Inc_C] = new ALUInstruction(WriteType.C, ALUOpType.Inc, FetchType.C);
            _instructions[Opcode.Inc_D] = new ALUInstruction(WriteType.D, ALUOpType.Inc, FetchType.D);
            _instructions[Opcode.Inc_E] = new ALUInstruction(WriteType.E, ALUOpType.Inc, FetchType.E);
            _instructions[Opcode.Inc_H] = new ALUInstruction(WriteType.H, ALUOpType.Inc, FetchType.H);
            _instructions[Opcode.Inc_L] = new ALUInstruction(WriteType.L, ALUOpType.Inc, FetchType.L);
            _instructions[Opcode.Inc_XHL] = new ALUInstruction(WriteType.XHL, ALUOpType.Inc, FetchType.XHL);

            _instructions[Opcode.Inc_BC] = new ALUInstruction(WriteType.BC, ALUOpType.Inc16, FetchType.BC);
            _instructions[Opcode.Inc_DE] = new ALUInstruction(WriteType.DE, ALUOpType.Inc16, FetchType.DE);
            _instructions[Opcode.Inc_HL] = new ALUInstruction(WriteType.HL, ALUOpType.Inc16, FetchType.HL);
            _instructions[Opcode.Inc_SP] = new ALUInstruction(WriteType.SP, ALUOpType.Inc16, FetchType.SP);
        }

        private (ushort, ushort) Inc(ushort input)
        {
            var result = input + 1;

            if ((result & 0x10) != 0x00)
            {
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            SetFlag(Flag.Subtract);

            if ((byte)result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            return ((ushort)result, 4);
        }

        private (ushort, ushort) Inc16(ushort input)
        {
            var result = input + 1;

            return ((ushort)result, 8);
        }
    }
}