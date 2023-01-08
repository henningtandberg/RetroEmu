using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
		    _instructions[0x88] = &AdcB;
			_instructions[0x89] = &AdcC;
			_instructions[0x8A] = &AdcD;
			_instructions[0x8B] = &AdcE;
			_instructions[0x8C] = &AdcH;
			_instructions[0x8D] = &AdcL;
			_instructions[0x8E] = &AdcValueFromAddress;
			_instructions[0x8F] = &AdcA;
			_instructions[0xCE] = &AdcValueFromNextOpcode;
	    }
	    
	    private static byte AdcA(Processor processor) => processor.Adc(*processor.Registers.A);
	    private static byte AdcB(Processor processor) => processor.Adc(*processor.Registers.B);
	    private static byte AdcC(Processor processor) => processor.Adc(*processor.Registers.C);
	    private static byte AdcD(Processor processor) => processor.Adc(*processor.Registers.D);
	    private static byte AdcE(Processor processor) => processor.Adc(*processor.Registers.E);
	    private static byte AdcH(Processor processor) => processor.Adc(*processor.Registers.H);
	    private static byte AdcL(Processor processor) => processor.Adc(*processor.Registers.L);
	    private static byte AdcValueFromAddress(Processor processor) => processor.AdcValueFromAddress();
	    private static byte AdcValueFromNextOpcode(Processor processor) => processor.AdcValueFromNextOpcode();
	    
		private byte Adc(byte value)
		{
			var carry = IsSet(Flag.Carry) ? 1 : 0;
			var registerA = *Registers.A;
			int result;
			
			unchecked
			{
				result = registerA + value + carry;
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

		private byte AdcValueFromAddress()
		{
			var value = _memory.Get(*Registers.HL);
			return (byte)(Adc(value) + 4);
		}
		
		private byte AdcValueFromNextOpcode()
		{
			var value = GetNextOpcode();
			return (byte)(Adc(value) + 4);
		}
    }
}