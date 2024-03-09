using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
            _ops[(int)OpType.Adc] = &Adc;

            // TODO: More compact way of writing this?
            _instructions[OPC.Adc_A_B] = new Instruction(WriteType.A, OpType.Adc, FetchType.B);
            _instructions[OPC.Adc_A_C] = new Instruction(WriteType.A, OpType.Adc, FetchType.C);
            _instructions[OPC.Adc_A_D] = new Instruction(WriteType.A, OpType.Adc, FetchType.D);
            _instructions[OPC.Adc_A_E] = new Instruction(WriteType.A, OpType.Adc, FetchType.E);
            _instructions[OPC.Adc_A_H] = new Instruction(WriteType.A, OpType.Adc, FetchType.H);
            _instructions[OPC.Adc_A_L] = new Instruction(WriteType.A, OpType.Adc, FetchType.L);
            _instructions[OPC.Adc_A_XHL] = new Instruction(WriteType.A, OpType.Adc, FetchType.XHL);
            _instructions[OPC.Adc_A_A] = new Instruction(WriteType.A, OpType.Adc, FetchType.A);
            _instructions[OPC.Adc_A_N8] = new Instruction(WriteType.A, OpType.Adc, FetchType.N8);
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