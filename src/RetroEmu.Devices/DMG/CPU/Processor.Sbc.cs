using System;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupSbcInstructions()
		{
			_ops[(int)OpType.Sbc] = &Sbc;

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

		private static (byte, ushort) Sbc(Processor processor, ushort value)
        {
            var carry = processor.IsSet(Flag.Carry) ? 1 : 0;
            var registerA = *processor.Registers.A;
			var result = (int)registerA - (int)registerA - (int)carry;

            if (result == 0)
            {
                processor.SetFlag(Flag.Zero);
            }
            else
            {
                processor.ClearFlag(Flag.Zero);
            }

            processor.SetFlag(Flag.Subtract);

            if ((registerA & 0x0F) - (value & 0x0F) < 0) // TODO: Doublecheck if this is correct
            {
                processor.SetFlag(Flag.HalfCarry);
            }
            else
            {
                processor.ClearFlag(Flag.HalfCarry);
            }

            if (result < 0)
			{
                processor.SetFlag(Flag.Carry);
            }
            else
            {
                processor.ClearFlag(Flag.Carry);
            }

			return (4, (ushort)result); // cycles
		}
    }
}