using System;
using RetroEmu.Devices.DMG.CPU.Instructions;

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
            _instructions[Opcode.Add_A_B] = new Instruction(WriteType.A, OpType.Add, FetchType.B);
            _instructions[Opcode.Add_A_C] = new Instruction(WriteType.A, OpType.Add, FetchType.C);
			_instructions[Opcode.Add_A_D] = new Instruction(WriteType.A, OpType.Add, FetchType.D);
			_instructions[Opcode.Add_A_E] = new Instruction(WriteType.A, OpType.Add, FetchType.E);
			_instructions[Opcode.Add_A_H] = new Instruction(WriteType.A, OpType.Add, FetchType.H);
			_instructions[Opcode.Add_A_L] = new Instruction(WriteType.A, OpType.Add, FetchType.L);
			_instructions[Opcode.Add_A_XHL] = new Instruction(WriteType.A, OpType.Add, FetchType.XHL);
			_instructions[Opcode.Add_A_A] = new Instruction(WriteType.A, OpType.Add, FetchType.A);
			_instructions[Opcode.Add_A_N8] = new Instruction(WriteType.A, OpType.Add, FetchType.N8);

			_instructions[Opcode.Add_HL_BC] = new Instruction(WriteType.HL, OpType.Add16, FetchType.BC);
			_instructions[Opcode.Add_HL_DE] = new Instruction(WriteType.HL, OpType.Add16, FetchType.DE);
			_instructions[Opcode.Add_HL_HL] = new Instruction(WriteType.HL, OpType.Add16, FetchType.HL);
			_instructions[Opcode.Add_HL_SP] = new Instruction(WriteType.HL, OpType.Add16, FetchType.SP);

			_instructions[Opcode.Add_SP_N8] = new Instruction(WriteType.SP, OpType.AddSP, FetchType.N8);
        }

	    private static OperationOutput Add(Processor processor, IOperationInput operationInput) => processor.Add(operationInput);
        private static OperationOutput Add16(Processor processor, IOperationInput operationInput) => processor.Add16(operationInput);
	    private static OperationOutput AddSP(Processor processor, IOperationInput operationInput) => processor.AddSP(operationInput);
        
		private OperationOutput Add(IOperationInput operationInput)
		{
			var registerA = *Registers.A;
			var result = (int)registerA + (int)operationInput.Value;

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

            return new OperationOutput((ushort)result, 4);
        }

        private OperationOutput Add16(IOperationInput operationInput)
        {
            var registerHL = *Registers.HL;
            var result = (int)registerHL + (int)operationInput.Value;

            if (result > 0xFFFF)
            {
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

            if (result > 0x0FFF)
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

            return new OperationOutput((ushort)result, 8);
        }

        private OperationOutput AddSP(IOperationInput operationInput)
        {
            var registerSP = *Registers.SP;
            var result = (int)registerSP + (int)operationInput.Value;

            if (result > 0xFFFF) // Set or reset according to operation?
            {
                SetFlag(Flag.Carry);
            }
            else
            {
                ClearFlag(Flag.Carry);
            }

            if (result > 0x0FFF) // Set or reset according to operation?
            {
                SetFlag(Flag.HalfCarry);
            }
            else
            {
                ClearFlag(Flag.HalfCarry);
            }

            ClearFlag(Flag.Subtract);
            ClearFlag(Flag.Zero);

            return new OperationOutput((ushort)result, 12); // cycles (Not sure why this one is more expensive)
        }
    }
}