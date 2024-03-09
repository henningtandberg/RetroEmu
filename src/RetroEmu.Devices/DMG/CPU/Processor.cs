namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
		private enum FetchType : byte
        {
			// 8-bit
            A, B, C, D, E, H, L, // Get value directly from register
            XBC, XDE, XHL, // Load value from address stored in double register
			XHLD, XHLI, // Load value from address at HL and increment/decrement HL
            XN16, // Load value from address in the next two opcodes
            N8, // Get value from the next opcode
			XN8, // Get value from the next opcode + 0xFF00.
			XC, // Get value from register C + 0xFF00.

            // 16-bit
            BC, DE, HL, SP,
			N16
        }

		// TODO: Better name for this enum?
		private enum OpType : byte
		{
            Add,
            Add16,
            AddSP,
			Adc,
			Dec,
			Ld,
			JpNZ,
        }

		private enum WriteType : byte
		{
			// 8-bit
			A, B, C, D, E, H, L,
            XBC, XDE, XHL, // Store value in memory at address stored in double register
            XHLD, XHLI, // Store value in memory at address stored in HL, then increment/decrement HL
            XN16, // Store value in memory at address in the next two opcodes
            XC, // Store at address C + 0xFF00, TODO: Better name?
            XN8, // Store at next opcode + 0xFF00, TODO: Better name?

            // 16-bit
            HL, SP, PC
		}

		private record Instruction(WriteType WriteOp, OpType Op, FetchType FetchOp)
        {
            public readonly WriteType WriteOp = WriteOp;
            public readonly OpType Op = Op;
            public readonly FetchType FetchOp = FetchOp;
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
			SetupDecInstructions();
			SetupLdInstructions();
			SetupJpInstructions();
			SetupWrite();
		}

		public void Reset()
		{
		}

		public int Update()
		{
			var opcode = GetNextOpcode();
			var instr = _instructions[opcode];
			var (fetchCycles, fetchResult) = _fetchOps[(int)instr.FetchOp](this);
            var (opCycles, opResult) = _ops[(int)instr.Op](this, fetchResult);
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