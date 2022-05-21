using System;

namespace RetroEmu.Devices.DMG
{
	public unsafe class Processor : IProcessor
	{
		private readonly IMemory _memory;
		private readonly delegate* managed<Processor, byte, byte>[] _instructions;

		public Registers Registers { get; }

		public Processor(IMemory memory)
		{
			_memory = memory;
			_instructions = new delegate* managed<Processor, byte, byte>[256];
			_instructions[0x80] = &Add;
			_instructions[0x81] = &Add;
			_instructions[0x82] = &Add;
			_instructions[0x83] = &Add;
			_instructions[0x84] = &Add;
			_instructions[0x85] = &Add;
			_instructions[0x86] = &Add;
			_instructions[0x87] = &Add;
			_instructions[0xC6] = &Add;
			Registers = new Registers();
		}

		public void Reset()
		{
		}

		public int Update()
		{
			var opcode = GetNextOpcode();
			return _instructions[opcode](this, opcode);
		}

		private byte GetNextOpcode()
		{
			var opcode = _memory.Get(*Registers.PC);
			(*Registers.PC)++;
			return opcode;
		}

		private static byte Add(Processor processor, byte opcode) => processor.Add(opcode);
		private byte Add(byte opcode)
		{
			var setFlagH = false;
			var setFlagC = false;
			byte cycles = 4;
			
			switch (opcode)
			{
				case 0x80:
					*Registers.A = Add(*Registers.A, *Registers.B, out setFlagH, out setFlagC);
					break;
				case 0x81:
					*Registers.A = Add(*Registers.A, *Registers.C, out setFlagH, out setFlagC);
					break;
				case 0x82:
					*Registers.A = Add(*Registers.A, *Registers.D, out setFlagH, out setFlagC);
					break;
				case 0x83:
					*Registers.A = Add(*Registers.A, *Registers.E, out setFlagH, out setFlagC);
					break;
				case 0x84:
					*Registers.A = Add(*Registers.A, *Registers.H, out setFlagH, out setFlagC);
					break;
				case 0x85:
					*Registers.A = Add(*Registers.A, *Registers.L, out setFlagH, out setFlagC);
					break;
				case 0x86:
				{
					*Registers.A = Add(*Registers.A, *Registers.H, out _, out _);
					*Registers.A = Add(*Registers.A, *Registers.L, out setFlagH, out setFlagC);
					cycles = 8;
					break;
				}
				case 0x87:
					*Registers.A = Add(*Registers.A, *Registers.A, out setFlagH, out setFlagC);
					break;
				case 0xC6:
				{
					var value = GetNextOpcode();
					*Registers.A = Add(*Registers.A, value, out setFlagH, out setFlagC);
					cycles = 8;
					break;
				}
				default:
					throw new Exception($"Opcode {opcode} should not result in an ADD operation");
			};

			if (setFlagH)
			{
			}

			if (setFlagC)
			{
			}

			return cycles;
		}

		private static byte Add(byte a, byte b, out bool setFlagH, out bool setFlagC)
		{
			var c = (byte)(a + b);
			setFlagH = (c & 0xF0) > 0;
			setFlagC = ((a ^ b) >= 0) & ((a ^ c) < 0);

			return c;
		}
		
	}
}