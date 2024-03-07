using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
            _ops[(int)OpType.Adc] = &Adc;

            // TODO: More compact way of writing this?
            _instructions[0x88] = new Instruction(FetchType.RegB, OpType.Adc, WriteType.RegA);
            _instructions[0x89] = new Instruction(FetchType.RegC, OpType.Adc, WriteType.RegA);
            _instructions[0x8A] = new Instruction(FetchType.RegD, OpType.Adc, WriteType.RegA);
            _instructions[0x8B] = new Instruction(FetchType.RegE, OpType.Adc, WriteType.RegA);
            _instructions[0x8C] = new Instruction(FetchType.RegH, OpType.Adc, WriteType.RegA);
            _instructions[0x8D] = new Instruction(FetchType.RegL, OpType.Adc, WriteType.RegA);
            _instructions[0x8E] = new Instruction(FetchType.AddressHL, OpType.Adc, WriteType.RegA);
            _instructions[0x8F] = new Instruction(FetchType.RegA, OpType.Adc, WriteType.RegA);
            _instructions[0xCE] = new Instruction(FetchType.ImmediateValue, OpType.Adc, WriteType.RegA);
        }
	    
		private static (byte, ushort) Adc(Processor processor, ushort value)
		{
			var carry = processor.IsSet(Flag.Carry) ? 1 : 0;
			var registerA = *processor.Registers.A;
            var result = (int)registerA + (int)value + (int)carry;

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
			return (4, (ushort)result); // cycles
		}

    }
}