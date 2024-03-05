using System;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAddInstructions()
		{
			_ops[(int)OpType.Add] = &Add;

			// TODO: More compact way of writing this?
            _instructions[0x80] = new Instruction(FetchType.RegB, OpType.Add);
            _instructions[0x81] = new Instruction(FetchType.RegC, OpType.Add);
			_instructions[0x82] = new Instruction(FetchType.RegD, OpType.Add);
			_instructions[0x83] = new Instruction(FetchType.RegE, OpType.Add);
			_instructions[0x84] = new Instruction(FetchType.RegH, OpType.Add);
			_instructions[0x85] = new Instruction(FetchType.RegL, OpType.Add);
			_instructions[0x86] = new Instruction(FetchType.AddressHL, OpType.Add);
			_instructions[0x87] = new Instruction(FetchType.RegA, OpType.Add);
			_instructions[0xC6] = new Instruction(FetchType.ImmediateValue, OpType.Add);
        }

		private static (byte, byte) Add(Processor processor, byte value)
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

			*processor.Registers.A = (byte)result;
			return (4, (byte)result); // cycles
		}
	}
}