using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
            _ops[(int)OpType.Adc] = &Adc;

            // TODO: More compact way of writing this?
            _instructions[OPC.Adc_A_B] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegB);
            _instructions[OPC.Adc_A_C] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegC);
            _instructions[OPC.Adc_A_D] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegD);
            _instructions[OPC.Adc_A_E] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegE);
            _instructions[OPC.Adc_A_H] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegH);
            _instructions[OPC.Adc_A_L] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegL);
            _instructions[OPC.Adc_A_XHL] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.AddressHL);
            _instructions[OPC.Adc_A_A] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.RegA);
            _instructions[OPC.Adc_A_N8] = new Instruction(WriteType.RegA, OpType.Adc, FetchType.ImmediateValue);
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