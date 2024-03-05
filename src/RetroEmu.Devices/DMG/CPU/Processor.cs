using System;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
		private enum FetchType : byte
        {
			// 8-bit
            RegA, RegB, RegC, RegD, RegE, RegH, RegL, // Get value directly from register
            AddressBC, AddressDE, AddressHL, // Load value from address stored in double register
			AddressHL_Dec, AddressHL_Inc, // Load value from address at HL and increment/decrement HL
            ImmediateAddress, // Load value from address in the next two opcodes
            ImmediateValue, // Get value from the next two opcodes
			Address_Immediate_0xFF00, // Get value from the next opcode + 0xFF00, TODO: Better name?

            // 16-bit
            RegBC, RegDE, RegHL, RegSP,
			ImmediateValue16
        }

		// TODO: Better name for this enum?
		private enum OpType : byte
		{
            Add,
            Add16,
            AddSP,
			Adc
        }

		private enum WriteType : byte
		{
			// 8-bit
			RegA, RegB, RegC, RegD, RegE, RegH, RegL,
            AddressBC, AddressDE, AddressHL, // Store value in memory at address stored in double register
            AddressHL_Dec, AddressHL_Inc, // Store value in memory at address stored in HL, then increment/decrement HL
            ImmediateAddress, // Store value in memory at address in the next two opcodes
            Address_RegC_0xFF00, // Store at address C + 0xFF00, TODO: Better name?
            Address_Immediate_0xFF00, // Store at next opcode + 0xFF00, TODO: Better name?

            // 16-bit
            RegHL, RegSP,
			Count
		}

		private record Instruction(FetchType FetchOp, OpType Op, WriteType WriteOp)
		{
			public readonly FetchType FetchOp = FetchOp;
            public readonly OpType Op = Op;
            public readonly WriteType WriteOp = WriteOp;
		}

		private readonly IMemory _memory;
		private readonly Instruction[] _instructions;
        private readonly delegate* managed<Processor, (byte, ushort)>[] _fetchOps;
        private readonly delegate* managed<Processor, ushort, (byte, ushort)>[] _ops;
        private readonly delegate* managed<Processor, ushort, byte>[] _writeOps;

		public Registers Registers { get; }

		public Processor(IMemory memory)
		{
			Registers = new Registers();
			_memory = memory;
			_instructions = new Instruction[256];
            _fetchOps = new delegate* managed<Processor, (byte, ushort)>[EnumImplementation.Size<FetchType>()];
            _ops = new delegate* managed<Processor, ushort, (byte, ushort)>[EnumImplementation.Size<OpType>()];
            _writeOps = new delegate* managed<Processor, ushort, byte>[EnumImplementation.Size<WriteType>()];
			SetUpInstructions();
		}

		private void SetUpInstructions()
		{
			SetupFetch();
			SetupAddInstructions();
			SetupAdcInstructions();
			SetupWrite();
		}

		public void Reset()
		{
		}

		public int Update()
		{
			var opcode = GetNextOpcode();
			Instruction instr = _instructions[opcode];
			(var fetchCycles, var fetchResult) = _fetchOps[(int)instr.FetchOp](this);
            (var opCycles, var opResult) = _ops[(int)instr.Op](this, fetchResult);
            var writeCycles = _writeOps[(int)instr.WriteOp](this, opResult);

            return fetchCycles + opCycles + writeCycles;
		}

		private byte GetNextOpcode()
		{
			var opcode = _memory.Read(*Registers.PC);
			(*Registers.PC)++;
			return opcode;
		}
	}
}