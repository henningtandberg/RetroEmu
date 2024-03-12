using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupSubInstructions()
		{
            _instructions[Opcode.Sub_A_B] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.B);
            _instructions[Opcode.Sub_A_C] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.C);
			_instructions[Opcode.Sub_A_D] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.D);
			_instructions[Opcode.Sub_A_E] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.E);
			_instructions[Opcode.Sub_A_H] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.H);
			_instructions[Opcode.Sub_A_L] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.L);
			_instructions[Opcode.Sub_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.XHL);
			_instructions[Opcode.Sub_A_A] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.A);
			_instructions[Opcode.Sub_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.Sub, FetchType.N8);
        }

		private (ushort, ushort) Sub(ushort input)
		{
			var registerA = *Registers.A;
			var result = (int)registerA - (int)input;

            if (result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            SetFlag(Flag.Subtract);

            if ((registerA & 0x0F) - (input & 0x0F) < 0) // TODO: Doublecheck if this is correct
            {
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            if (result < 0)
			{
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

			return ((ushort)result, 4);
		}
    }
}