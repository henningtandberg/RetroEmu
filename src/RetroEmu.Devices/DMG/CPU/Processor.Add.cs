using System;

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
            _instructions[OPC.Add_A_B] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegB);
            _instructions[OPC.Add_A_C] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegC);
			_instructions[OPC.Add_A_D] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegD);
			_instructions[OPC.Add_A_E] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegE);
			_instructions[OPC.Add_A_H] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegH);
			_instructions[OPC.Add_A_L] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegL);
			_instructions[OPC.Add_A_XHL] = new Instruction(WriteType.RegA, OpType.Add, FetchType.AddressHL);
			_instructions[OPC.Add_A_A] = new Instruction(WriteType.RegA, OpType.Add, FetchType.RegA);
			_instructions[OPC.Add_A_N8] = new Instruction(WriteType.RegA, OpType.Add, FetchType.ImmediateValue);

			_instructions[OPC.Add_HL_BC] = new Instruction(WriteType.RegHL, OpType.Add16, FetchType.RegBC);
			_instructions[OPC.Add_HL_DE] = new Instruction(WriteType.RegHL, OpType.Add16, FetchType.RegDE);
			_instructions[OPC.Add_HL_HL] = new Instruction(WriteType.RegHL, OpType.Add16, FetchType.RegHL);
			_instructions[OPC.Add_HL_SP] = new Instruction(WriteType.RegHL, OpType.Add16, FetchType.RegSP);

			_instructions[OPC.Add_SP_N8] = new Instruction(WriteType.RegSP, OpType.AddSP, FetchType.ImmediateValue);
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