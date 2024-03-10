using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAddInstructions()
		{
			_ops[(int)OpType.Add] = &Add;
			_ops[(int)OpType.Add16] = &Add16;
			_ops[(int)OpType.AddSP] = &AddSP;

			// TODO: More compact way of writing this?
            _instructions[Opcode.Add_A_B] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.B);
            _instructions[Opcode.Add_A_C] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.C);
			_instructions[Opcode.Add_A_D] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.D);
			_instructions[Opcode.Add_A_E] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.E);
			_instructions[Opcode.Add_A_H] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.H);
			_instructions[Opcode.Add_A_L] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.L);
			_instructions[Opcode.Add_A_XHL] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.XHL);
			_instructions[Opcode.Add_A_A] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.A);
			_instructions[Opcode.Add_A_N8] = new GeneralInstruction(WriteType.A, OpType.Add, FetchType.N8);

			_instructions[Opcode.Add_HL_BC] = new GeneralInstruction(WriteType.HL, OpType.Add16, FetchType.BC);
			_instructions[Opcode.Add_HL_DE] = new GeneralInstruction(WriteType.HL, OpType.Add16, FetchType.DE);
			_instructions[Opcode.Add_HL_HL] = new GeneralInstruction(WriteType.HL, OpType.Add16, FetchType.HL);
			_instructions[Opcode.Add_HL_SP] = new GeneralInstruction(WriteType.HL, OpType.Add16, FetchType.SP);

			_instructions[Opcode.Add_SP_N8] = new GeneralInstruction(WriteType.SP, OpType.AddSP, FetchType.N8);
        }

		private static (byte, ushort) Add(Processor processor, ushort value)
		{
			var registerA = *processor.Registers.A;
			var result = (int)registerA + (int)value;

			if (result > 0xFF)
			{
                processor.SetFlag(Flag.Carry);
            }
            else
            {
                processor.ClearFlag(Flag.Carry);
            }

            if (result > 0x0F)
			{
                processor.SetFlag(Flag.HalfCarry);
            }
            else
            {
                processor.ClearFlag(Flag.HalfCarry);
            }

            processor.ClearFlag(Flag.Subtract);

			if (result == 0)
			{
                processor.SetFlag(Flag.Zero);
			}
            else
            {
                processor.ClearFlag(Flag.Zero);
            }

			return (4, (ushort)result); // cycles
		}

        private static (byte, ushort) Add16(Processor processor, ushort value)
        {
            var registerHL = *processor.Registers.HL;
            var result = (int)registerHL + (int)value;

            if (result > 0xFFFF)
            {
                processor.SetFlag(Flag.Carry);
            }
            else
            {
                processor.ClearFlag(Flag.Carry);
            }

            if (result > 0x0FFF)
            {
                processor.SetFlag(Flag.HalfCarry);
            }
            else
            {
                processor.ClearFlag(Flag.HalfCarry);
            }

            processor.ClearFlag(Flag.Subtract);

            if (result == 0)
            {
                processor.SetFlag(Flag.Zero);
            }
            else
            {
                processor.ClearFlag(Flag.Zero);
            }

            return (8, (ushort)result); // cycles
        }

        private static (byte, ushort) AddSP(Processor processor, ushort value)
        {
            var registerSP = *processor.Registers.SP;
            var result = (int)registerSP + (int)value;

            if (result > 0xFFFF) // Set or reset according to operation?
            {
                processor.SetFlag(Flag.Carry);
            }
            else
            {
                processor.ClearFlag(Flag.Carry);
            }

            if (result > 0x0FFF) // Set or reset according to operation?
            {
                processor.SetFlag(Flag.HalfCarry);
            }
            else
            {
                processor.ClearFlag(Flag.HalfCarry);
            }

            processor.ClearFlag(Flag.Subtract);
            processor.ClearFlag(Flag.Zero);

            return (12, (ushort)result); // cycles (Not sure why this one is more expensive)
        }
    }
}