using System;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupSubInstructions()
		{
			_ops[(int)OpType.Sub] = &Sub;

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

		private static (byte, ushort) Sub(Processor processor, ushort value)
		{
			var registerA = *processor.Registers.A;
			var result = (int)registerA - (int)registerA;

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