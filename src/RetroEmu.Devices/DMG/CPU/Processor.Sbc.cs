using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupSbcInstructions()
		{
            _instructions[Opcode.Sbc_A_B] = new Instruction(WriteType.A, OpType.Sbc, FetchType.B);
            _instructions[Opcode.Sbc_A_C] = new Instruction(WriteType.A, OpType.Sbc, FetchType.C);
			_instructions[Opcode.Sbc_A_D] = new Instruction(WriteType.A, OpType.Sbc, FetchType.D);
			_instructions[Opcode.Sbc_A_E] = new Instruction(WriteType.A, OpType.Sbc, FetchType.E);
			_instructions[Opcode.Sbc_A_H] = new Instruction(WriteType.A, OpType.Sbc, FetchType.H);
			_instructions[Opcode.Sbc_A_L] = new Instruction(WriteType.A, OpType.Sbc, FetchType.L);
			_instructions[Opcode.Sbc_A_XHL] = new Instruction(WriteType.A, OpType.Sbc, FetchType.XHL);
			_instructions[Opcode.Sbc_A_A] = new Instruction(WriteType.A, OpType.Sbc, FetchType.A);
			_instructions[Opcode.Sbc_A_N8] = new Instruction(WriteType.A, OpType.Sbc, FetchType.N8);
        }

		private OperationOutput Sbc(IOperationInput operationInput)
        {
            var carry = IsSet(Flag.Carry) ? 1 : 0;
            var registerA = *Registers.A;
			var result = (int)registerA - (int)registerA - (int)carry;

            if (result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            SetFlag(Flag.Subtract);

            if ((registerA & 0x0F) - (operationInput.Value & 0x0F) < 0) // TODO: Doublecheck if this is correct
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

			return new OperationOutput((ushort)result, 4);
		}
    }
}