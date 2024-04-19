using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupXorInstructions()
		{
            _instructions[Opcode.Xor_A_B] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.B);
            _instructions[Opcode.Xor_A_C] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.C);
			_instructions[Opcode.Xor_A_D] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.D);
			_instructions[Opcode.Xor_A_E] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.E);
			_instructions[Opcode.Xor_A_H] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.H);
			_instructions[Opcode.Xor_A_L] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.L);
			_instructions[Opcode.Xor_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.XHL);
			_instructions[Opcode.Xor_A_A] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.A);
			_instructions[Opcode.Xor_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.Xor, FetchType.N8);
        }

		private (ushort, ushort) Xor(ushort input)
        {
            var registerA = *Registers.A;
			var result = registerA ^ input;

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