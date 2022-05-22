using System;

namespace RetroEmu.Devices.DMG.CPU
{
    public unsafe partial class Processor
    {
	    private partial void SetUpInstructions()
	    {
		    _instructions[0x80] = &Add;
			_instructions[0x81] = &Add;
			_instructions[0x82] = &Add;
			_instructions[0x83] = &Add;
			_instructions[0x84] = &Add;
			_instructions[0x85] = &Add;
			_instructions[0x86] = &Add;
			_instructions[0x87] = &Add;
			_instructions[0xC6] = &Add;
	    }

		private static byte Add(Processor processor, byte opcode) => processor.Add(opcode);
		
		private byte Add(byte opcode)
		{
			var setHalfCarryFlag = false;
			var setCarryFlag = false;
			byte cycles = 4;
			
			switch (opcode)
			{
				case 0x80:
					*Registers.A = Add(*Registers.A, *Registers.B, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0x81:
					*Registers.A = Add(*Registers.A, *Registers.C, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0x82:
					*Registers.A = Add(*Registers.A, *Registers.D, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0x83:
					*Registers.A = Add(*Registers.A, *Registers.E, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0x84:
					*Registers.A = Add(*Registers.A, *Registers.H, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0x85:
					*Registers.A = Add(*Registers.A, *Registers.L, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0x86:
				{
					var value = _memory.Get(*Registers.HL);
					*Registers.A = Add(*Registers.A, value, out setHalfCarryFlag, out setCarryFlag);
					cycles = 8;
					break;
				}
				case 0x87:
					*Registers.A = Add(*Registers.A, *Registers.A, out setHalfCarryFlag, out setCarryFlag);
					break;
				case 0xC6:
				{
					var value = GetNextOpcode();
					*Registers.A = Add(*Registers.A, value, out setHalfCarryFlag, out setCarryFlag);
					cycles = 8;
					break;
				}
				default:
					throw new Exception($"Opcode {opcode} should not result in an ADD operation");
			}

			if (setCarryFlag)
			{
				SetFlag(Flag.Carry);
			}

			if (setHalfCarryFlag)
			{
				SetFlag(Flag.HalfCarry);
			}
			
			ClearFlag(Flag.Subtract);

			if (*Registers.A == 0)
			{
				SetFlag(Flag.Zero);
			}
			
			return cycles;
		}

		private static byte Add(byte a, byte b, out bool setHalfCarryFlag, out bool setCarryFlag)
		{
			byte c;
			
			unchecked
			{
				c = (byte)(a + b);
				setHalfCarryFlag = (c & 0xF0) > 0;
				setCarryFlag = ((a ^ c) & ~(a ^ b)) >> 7 > 0;
			}

			return c;
		}
    }
}