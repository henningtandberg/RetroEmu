using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupSubInstructions()
		{
            _instructions[Opcode.Sub_A_B] = new Instruction(WriteType.A, OpType.Sub, FetchType.B);
            _instructions[Opcode.Sub_A_C] = new Instruction(WriteType.A, OpType.Sub, FetchType.C);
			_instructions[Opcode.Sub_A_D] = new Instruction(WriteType.A, OpType.Sub, FetchType.D);
			_instructions[Opcode.Sub_A_E] = new Instruction(WriteType.A, OpType.Sub, FetchType.E);
			_instructions[Opcode.Sub_A_H] = new Instruction(WriteType.A, OpType.Sub, FetchType.H);
			_instructions[Opcode.Sub_A_L] = new Instruction(WriteType.A, OpType.Sub, FetchType.L);
			_instructions[Opcode.Sub_A_XHL] = new Instruction(WriteType.A, OpType.Sub, FetchType.XHL);
			_instructions[Opcode.Sub_A_A] = new Instruction(WriteType.A, OpType.Sub, FetchType.A);
			_instructions[Opcode.Sub_A_N8] = new Instruction(WriteType.A, OpType.Sub, FetchType.N8);
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