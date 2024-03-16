using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupSbcInstructions()
		{
            _instructions[Opcode.Sbc_A_B] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.B);
            _instructions[Opcode.Sbc_A_C] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.C);
			_instructions[Opcode.Sbc_A_D] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.D);
			_instructions[Opcode.Sbc_A_E] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.E);
			_instructions[Opcode.Sbc_A_H] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.H);
			_instructions[Opcode.Sbc_A_L] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.L);
			_instructions[Opcode.Sbc_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.XHL);
			_instructions[Opcode.Sbc_A_A] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.A);
			_instructions[Opcode.Sbc_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.Sbc, FetchType.N8);
        }

		private (ushort, ushort) Sbc(ushort input)
        {
            var carry = IsSet(Flag.Carry) ? 1 : 0;
            var registerA = *Registers.A;
			var result = (int)registerA - (int)input - (int)carry;

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