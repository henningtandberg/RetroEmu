using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAndInstructions()
		{
            _instructions[Opcode.And_A_B] = new Instruction(WriteType.A, OpType.And, FetchType.B);
            _instructions[Opcode.And_A_C] = new Instruction(WriteType.A, OpType.And, FetchType.C);
			_instructions[Opcode.And_A_D] = new Instruction(WriteType.A, OpType.And, FetchType.D);
			_instructions[Opcode.And_A_E] = new Instruction(WriteType.A, OpType.And, FetchType.E);
			_instructions[Opcode.And_A_H] = new Instruction(WriteType.A, OpType.And, FetchType.H);
			_instructions[Opcode.And_A_L] = new Instruction(WriteType.A, OpType.And, FetchType.L);
			_instructions[Opcode.And_A_XHL] = new Instruction(WriteType.A, OpType.And, FetchType.XHL);
			_instructions[Opcode.And_A_A] = new Instruction(WriteType.A, OpType.And, FetchType.A);
			_instructions[Opcode.And_A_N8] = new Instruction(WriteType.A, OpType.And, FetchType.N8);
        }

		private OperationOutput And(IOperationInput operationInput)
        {
            var registerA = *Registers.A;
			var result = (int)registerA & (int)registerA;

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

			return new OperationOutput((ushort)result, 4);
		}
    }
}