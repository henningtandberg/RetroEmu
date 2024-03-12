using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
            _instructions[Opcode.Adc_A_B] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.B);
            _instructions[Opcode.Adc_A_C] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.C);
            _instructions[Opcode.Adc_A_D] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.D);
            _instructions[Opcode.Adc_A_E] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.E);
            _instructions[Opcode.Adc_A_H] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.H);
            _instructions[Opcode.Adc_A_L] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.L);
            _instructions[Opcode.Adc_A_XHL] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.XHL);
            _instructions[Opcode.Adc_A_A] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.A);
            _instructions[Opcode.Adc_A_N8] = new ALUInstruction(WriteType.A, ALUOpType.Adc, FetchType.N8);
        }
	    
		private (ushort, ushort) Adc(ushort input)
		{
			var carry = IsSet(Flag.Carry) ? 1 : 0;
			var registerA = *Registers.A;
            var result = (int)registerA + (int)input + (int)carry;

			if (result > 0xFF)
			{
                SetFlag(Flag.Carry);
			}
            else
            {
                ClearFlag(Flag.Carry);
            }

			if (result > 0x0F)
			{
                SetFlag(Flag.HalfCarry);
			}
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            ClearFlag(Flag.Subtract);

			if (result == 0)
			{
                SetFlag(Flag.Zero);
			}
            else
            {
                ClearFlag(Flag.Zero);
            }

            *Registers.A = (byte)result;
            return ((ushort)result, 4);
		}

    }
}