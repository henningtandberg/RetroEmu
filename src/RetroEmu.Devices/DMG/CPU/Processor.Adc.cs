using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
            _ops[(int)OpType.Adc] = &Adc;

            // TODO: More compact way of writing this?
            _instructions[OPC.Adc_A_B] = new Instruction(FetchType.RegB, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_C] = new Instruction(FetchType.RegC, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_D] = new Instruction(FetchType.RegD, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_E] = new Instruction(FetchType.RegE, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_H] = new Instruction(FetchType.RegH, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_L] = new Instruction(FetchType.RegL, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_XHL] = new Instruction(FetchType.AddressHL, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_A] = new Instruction(FetchType.RegA, OpType.Adc, WriteType.RegA);
            _instructions[OPC.Adc_A_N8] = new Instruction(FetchType.ImmediateValue, OpType.Adc, WriteType.RegA);
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

            *processor.Registers.A = (byte)result;
			return (4, (ushort)result); // cycles
		}

    }
}