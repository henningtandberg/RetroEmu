using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupOrInstructions()
		{
            _instructions[Opcode.Or_A_B] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.B);
            _instructions[Opcode.Or_A_C] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.C);
			_instructions[Opcode.Or_A_D] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.D);
			_instructions[Opcode.Or_A_E] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.E);
			_instructions[Opcode.Or_A_H] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.H);
			_instructions[Opcode.Or_A_L] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.L);
			_instructions[Opcode.Or_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.XHL);
			_instructions[Opcode.Or_A_A] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.A);
			_instructions[Opcode.Or_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.Or, FetchType.N8);
        }

		private (ushort, ushort) Or(ushort input)
        {
            var registerA = *Registers.A;
			var result = registerA | input;

            if (result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            ClearFlag(Flag.Subtract);
            ClearFlag(Flag.HalfCarry);
            ClearFlag(Flag.Carry);

			return ((ushort)result, 4);
		}
    }
}