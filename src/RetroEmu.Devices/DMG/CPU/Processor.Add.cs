using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAddInstructions()
		{
            _instructions[Opcode.Add_A_B] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.B);
            _instructions[Opcode.Add_A_C] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.C);
			_instructions[Opcode.Add_A_D] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.D);
			_instructions[Opcode.Add_A_E] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.E);
			_instructions[Opcode.Add_A_H] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.H);
			_instructions[Opcode.Add_A_L] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.L);
			_instructions[Opcode.Add_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.XHL);
			_instructions[Opcode.Add_A_A] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.A);
			_instructions[Opcode.Add_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.Add, FetchType.N8);

			_instructions[Opcode.Add_HL_BC] = new ALUInstruction(WriteType.HL, ALUOpType.Add16, FetchType.BC);
			_instructions[Opcode.Add_HL_DE] = new ALUInstruction(WriteType.HL, ALUOpType.Add16, FetchType.DE);
			_instructions[Opcode.Add_HL_HL] = new ALUInstruction(WriteType.HL, ALUOpType.Add16, FetchType.HL);
			_instructions[Opcode.Add_HL_SP] = new ALUInstruction(WriteType.HL, ALUOpType.Add16, FetchType.SP);

			_instructions[Opcode.Add_SP_N8] = new ALUInstruction(WriteType.SP, ALUOpType.AddSP, FetchType.N8);
        }

		private (ushort, ushort) Add(ushort input)
		{
			var registerA = *Registers.A;
			var result = registerA + input;

			if (result > 0xFF)
			{
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

            if ((registerA & 0x0F) + (input & 0x0F) > 0x0F)
			{
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            ClearFlag(Flag.Subtract);

			if (result == 0)
			{
                SetFlag(Flag.Zero);
			}
            else
            {
                ClearFlag(Flag.Zero);
            }

            return ((ushort)result, 4);
        }

        private (ushort, ushort) Add16(ushort input)
        {
            var registerHL = *Registers.HL;
            var result = (int)registerHL + (int)input;

            if (result > 0xFFFF)
            {
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

            if (result > 0x0FFF)
            {
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            ClearFlag(Flag.Subtract);

            if (result == 0)
            {
                SetFlag(Flag.Zero);
            }
            else
            {
                ClearFlag(Flag.Zero);
            }

            return ((ushort)result, 8);
        }

        private (ushort, ushort) AddSP(ushort input)
        {
            var registerSP = *Registers.SP;
            var result = (int)registerSP + (int)input;

            if (result > 0xFFFF) // Set or reset according to operation?
            {
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

            if (result > 0x0FFF) // Set or reset according to operation?
            {
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            ClearFlag(Flag.Subtract);
            ClearFlag(Flag.Zero);

            return ((ushort)result, 12); // cycles (Not sure why this one is more expensive)
        }
    }
}