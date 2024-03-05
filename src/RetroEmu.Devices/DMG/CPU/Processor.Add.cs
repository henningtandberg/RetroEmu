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
            _instructions[0x80] = new Instruction(FetchType.RegB, OpType.Add, WriteType.RegA);
            _instructions[0x81] = new Instruction(FetchType.RegC, OpType.Add, WriteType.RegA);
			_instructions[0x82] = new Instruction(FetchType.RegD, OpType.Add, WriteType.RegA);
			_instructions[0x83] = new Instruction(FetchType.RegE, OpType.Add, WriteType.RegA);
			_instructions[0x84] = new Instruction(FetchType.RegH, OpType.Add, WriteType.RegA);
			_instructions[0x85] = new Instruction(FetchType.RegL, OpType.Add, WriteType.RegA);
			_instructions[0x86] = new Instruction(FetchType.AddressHL, OpType.Add, WriteType.RegA);
			_instructions[0x87] = new Instruction(FetchType.RegA, OpType.Add, WriteType.RegA);
			_instructions[0xC6] = new Instruction(FetchType.ImmediateValue, OpType.Add, WriteType.RegA);

			_instructions[0x09] = new Instruction(FetchType.RegBC, OpType.Add16, WriteType.RegHL);
			_instructions[0x19] = new Instruction(FetchType.RegDE, OpType.Add16, WriteType.RegHL);
			_instructions[0x29] = new Instruction(FetchType.RegHL, OpType.Add16, WriteType.RegHL);
			_instructions[0x39] = new Instruction(FetchType.RegSP, OpType.Add16, WriteType.RegHL);

			_instructions[0xE8] = new Instruction(FetchType.ImmediateValue, OpType.AddSP, WriteType.RegSP);
        }

		private static (byte, ushort) Add(Processor processor, ushort value)
		{
			var registerA = *processor.Registers.A;
			var result = (int)registerA + (int)value;

			if (result > 0xFF)
			{
                processor.SetFlag(Flag.Carry);
			}

			if (result > 0x0F)
			{
                processor.SetFlag(Flag.HalfCarry);
			}

            processor.ClearFlag(Flag.Subtract);

			if (result == 0)
			{
                processor.SetFlag(Flag.Zero);
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

            if (result > 0x0FFF)
            {
                processor.SetFlag(Flag.HalfCarry);
            }

            processor.ClearFlag(Flag.Subtract);

            if (result == 0)
            {
                processor.SetFlag(Flag.Zero);
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

            if (result > 0x0FFF) // Set or reset according to operation?
            {
                processor.SetFlag(Flag.HalfCarry);
            }

            processor.ClearFlag(Flag.Subtract);
            processor.ClearFlag(Flag.Zero);

            return (12, (ushort)result); // cycles (Not sure why this one is more expensive)
        }
    }
}