using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAndInstructions()
		{
            _instructions[Opcode.And_A_B] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.B);
            _instructions[Opcode.And_A_C] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.C);
			_instructions[Opcode.And_A_D] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.D);
			_instructions[Opcode.And_A_E] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.E);
			_instructions[Opcode.And_A_H] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.H);
			_instructions[Opcode.And_A_L] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.L);
			_instructions[Opcode.And_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.XHL);
			_instructions[Opcode.And_A_A] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.A);
			_instructions[Opcode.And_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.And, FetchType.N8);
        }

		private (ushort, ushort) And(ushort input)
        {
            var registerA = *Registers.A;
			var result = (int)registerA & (int)input;

            if (result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            ClearFlag(Flag.Subtract);
            SetFlag(Flag.HalfCarry);
            ClearFlag(Flag.Carry);

			return ((ushort)result, 4);
		}
    }
}