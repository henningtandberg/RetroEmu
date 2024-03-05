namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
        enum FetchType : byte
        {
            Invalid = 0, // TODO: Remove at some point.
            RegA, RegB, RegC, RegD, RegE, RegH, RegL, // Get value directly from register
            AddressBC, AddressDE, AddressHL, // Load value from address stored in double register 
            ImmediateAddress, // Load value from address in the next two opcodes
            ImmediateValue, // Get value from the next two opcodes
            Count // TODO: Any way to remove this?
        }

		// TODO: Better name for this enum?
        enum OpType : byte
		{
			Invalid = 0, // TODO: Remove at some point.
            Add,
			Adc,
			Count
        }

		struct Instruction
        {
			public Instruction(FetchType fetchOp, OpType op)
			{
				this.fetchOp = fetchOp;
				this.op = op;
			}

            public FetchType fetchOp;
            public OpType op;
			// TODO: Write type
		}

		private readonly IMemory _memory;
		private readonly Instruction[] _instructions;
        private readonly delegate* managed<Processor, (byte, byte)>[] _fetchOps;
        private readonly delegate* managed<Processor, byte, (byte, byte)>[] _ops;

		public Registers Registers { get; }

		public Processor(IMemory memory)
		{
			Registers = new Registers();
			_memory = memory;
			_instructions = new Instruction[256];
            _fetchOps = new delegate* managed<Processor, (byte, byte)>[(int)FetchType.Count];
            _ops = new delegate* managed<Processor, byte, (byte, byte)>[(int)OpType.Count];
			SetUpInstructions();
		}

		private void SetUpInstructions()
		{
			SetupFetch();
			SetupAddInstructions();
			SetupAdcInstructions();
		}

		public void Reset()
		{
		}

		public int Update()
		{
			var opcode = GetNextOpcode();
			Instruction instr = _instructions[opcode];
			(var fetchCycles, var fetchResult) = _fetchOps[(int)instr.fetchOp](this);
            (var opCycles, var opResult) = _ops[(int)instr.op](this, 0);

            return fetchCycles + opCycles;
		}

		private byte GetNextOpcode()
		{
			var opcode = _memory.Get(*Registers.PC);
			(*Registers.PC)++;
			return opcode;
		}
	}
}