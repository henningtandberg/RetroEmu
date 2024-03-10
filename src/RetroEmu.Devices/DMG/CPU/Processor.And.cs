using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAndInstructions()
		{
			_ops[(int)OpType.And] = &And;

            _instructions[Opcode.And_A_B] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.B);
            _instructions[Opcode.And_A_C] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.C);
			_instructions[Opcode.And_A_D] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.D);
			_instructions[Opcode.And_A_E] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.E);
			_instructions[Opcode.And_A_H] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.H);
			_instructions[Opcode.And_A_L] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.L);
			_instructions[Opcode.And_A_XHL] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.XHL);
			_instructions[Opcode.And_A_A] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.A);
			_instructions[Opcode.And_A_N8] = new GeneralInstruction(WriteType.A, OpType.And, FetchType.N8);
        }

		private static (byte, ushort) And(Processor processor, ushort value)
        {
            var registerA = *processor.Registers.A;
			var result = (int)registerA & (int)registerA;

            if (result == 0)
            {
                processor.SetFlag(Flag.Zero);
            }
            else
            {
                processor.ClearFlag(Flag.Zero);
            }

            processor.ClearFlag(Flag.Subtract);
            processor.SetFlag(Flag.HalfCarry);
            processor.ClearFlag(Flag.Carry);

			return (4, (ushort)result); // cycles
		}
    }
}