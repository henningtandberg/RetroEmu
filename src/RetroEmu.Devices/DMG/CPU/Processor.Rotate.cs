using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor
	{
		private void SetupRotateInstructions()
		{
            _instructions[Opcode.Rlc_A] = new RotationInstruction(WriteType.A, RotateOpType.RotateThroughCarry, FetchType.A, RotationDirection.Left);
            _instructions[Opcode.Rla] = new RotationInstruction(WriteType.A, RotateOpType.Rotate, FetchType.A, RotationDirection.Left);
            _instructions[Opcode.Rrc_A] = new RotationInstruction(WriteType.A, RotateOpType.RotateThroughCarry, FetchType.A, RotationDirection.Right);
            _instructions[Opcode.Rra] = new RotationInstruction(WriteType.A, RotateOpType.Rotate, FetchType.A, RotationDirection.Right);
        }

		private (ushort, ushort) RotateLeftThroughCarry(byte input)
		{
			var lsbMask = IsSet(Flag.Carry) ? 0x01 : 0;
			
			if ((input & 0x80) > 0)
			{
				SetFlag(Flag.Carry);
			}
			else
			{
				ClearFlag(Flag.Carry);
			}

			var result = (input << 1) | lsbMask;

			if (result == 0)
			{
				SetFlag(Flag.Zero);
			}
			
			ClearFlag(Flag.Subtract);
			ClearFlag(Flag.HalfCarry);
			
			return ((ushort)result, 4);
		}
		
		private (ushort, ushort) RotateRightThroughCarry(byte input)
		{
			var msbMask = IsSet(Flag.Carry) ? 0x80 : 0;
			
			if ((input & 0x01) > 0)
			{
				SetFlag(Flag.Carry);
			}
			else
			{
				ClearFlag(Flag.Carry);
			}

			var result = (input >> 1) | msbMask;

			if (result == 0)
			{
				SetFlag(Flag.Zero);
			}
			
			ClearFlag(Flag.Subtract);
			ClearFlag(Flag.HalfCarry);
			
			return ((ushort)result, 4);
		}
	}
}