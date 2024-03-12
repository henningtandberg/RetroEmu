using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
        
	    private void SetupAdcInstructions()
	    {
            _instructions[Opcode.Adc_A_B] = new Instruction(WriteType.A, OpType.Adc, FetchType.B);
            _instructions[Opcode.Adc_A_C] = new Instruction(WriteType.A, OpType.Adc, FetchType.C);
            _instructions[Opcode.Adc_A_D] = new Instruction(WriteType.A, OpType.Adc, FetchType.D);
            _instructions[Opcode.Adc_A_E] = new Instruction(WriteType.A, OpType.Adc, FetchType.E);
            _instructions[Opcode.Adc_A_H] = new Instruction(WriteType.A, OpType.Adc, FetchType.H);
            _instructions[Opcode.Adc_A_L] = new Instruction(WriteType.A, OpType.Adc, FetchType.L);
            _instructions[Opcode.Adc_A_XHL] = new Instruction(WriteType.A, OpType.Adc, FetchType.XHL);
            _instructions[Opcode.Adc_A_A] = new Instruction(WriteType.A, OpType.Adc, FetchType.A);
            _instructions[Opcode.Adc_A_N8] = new Instruction(WriteType.A, OpType.Adc, FetchType.N8);
        }
	    
		private OperationOutput Adc(IOperationInput operationInput)
		{
			var carry = IsSet(Flag.Carry) ? 1 : 0;
			var registerA = *Registers.A;
            var result = (int)registerA + (int)operationInput.Value + (int)carry;

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
            return new OperationOutput((ushort)result, 4);
		}

    }
}