using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupCpInstructions()
		{
			_instructions[Opcode.Cp_A_A] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.A);
            _instructions[Opcode.Cp_A_B] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.B);
            _instructions[Opcode.Cp_A_C] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.C);
			_instructions[Opcode.Cp_A_D] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.D);
			_instructions[Opcode.Cp_A_E] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.E);
			_instructions[Opcode.Cp_A_H] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.H);
			_instructions[Opcode.Cp_A_L] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.L);
			_instructions[Opcode.Cp_A_XHL] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.XHL);
			_instructions[Opcode.Cp_A_N8] = new ALUInstruction(WriteType.None, ALUOpType.Cp, FetchType.N8);
        }

		private (ushort, ushort) Cp(ushort input)
		{
			var registerA = *Registers.A;
			
			ClearAllFlags();
			SetFlag(Flag.Subtract);
			
			if (registerA - input == 0)
			{
				SetFlag(Flag.Zero);
			}
			
			if ((registerA & 0x0F) - (input & 0x0F) < 0) // TODO: Doublecheck if this is correct
			{
				SetFlag(Flag.HalfCarry);
			}

			if (registerA < input)
			{
				SetFlag(Flag.Carry);
			}

            return (0, 4);
        }
    }
}