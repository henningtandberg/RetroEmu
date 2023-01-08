using System;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupAddInstructions()
		{
			_instructions[0x80] = &AddB;
			_instructions[0x81] = &AddC;
			_instructions[0x82] = &AddD;
			_instructions[0x83] = &AddE;
			_instructions[0x84] = &AddH;
			_instructions[0x85] = &AddL;
			_instructions[0x86] = &AddValueFromAddress;
			_instructions[0x87] = &AddA;
			_instructions[0xC6] = &AddValueFromNextOpcode;
		}

		private static byte AddA(Processor processor) => processor.Add(*processor.Registers.A);
		private static byte AddB(Processor processor) => processor.Add(*processor.Registers.B);
		private static byte AddC(Processor processor) => processor.Add(*processor.Registers.C);
		private static byte AddD(Processor processor) => processor.Add(*processor.Registers.D);
		private static byte AddE(Processor processor) => processor.Add(*processor.Registers.E);
		private static byte AddH(Processor processor) => processor.Add(*processor.Registers.H);
		private static byte AddL(Processor processor) => processor.Add(*processor.Registers.L);
		private static byte AddValueFromAddress(Processor processor) => processor.AddValueFromAddress();
		private static byte AddValueFromNextOpcode(Processor processor) => processor.AddValueFromNextOpcode();

		private byte Add(byte value)
		{
			var registerA = *Registers.A;
			int result;

			unchecked
			{
				result = registerA + value;
			}

			if (result > 0xFF)
			{
				SetFlag(Flag.Carry);
			}

			if (result > 0x0F)
			{
				SetFlag(Flag.HalfCarry);
			}

			ClearFlag(Flag.Subtract);

			if (result == 0)
			{
				SetFlag(Flag.Zero);
			}

			*Registers.A = (byte)result;
			return 4; // cycles
		}

		private byte AddValueFromAddress()
		{
			var value = _memory.Get(*Registers.HL);
			return (byte)(Add(value) + 4);
		}

		private byte AddValueFromNextOpcode()
		{
			var value = GetNextOpcode();
			return (byte)(Add(value) + 4);
		}
	}
}