using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
		private readonly IMemory _memory;
		private readonly IInstruction[] _instructions;
        private readonly delegate* managed<Processor, (byte, ushort)>[] _fetchOps;
        private readonly delegate* managed<Processor, ushort, (byte, ushort)>[] _ops;
        private readonly delegate* managed<Processor, ushort, byte>[] _writeOps;

		public Registers Registers { get; }

		public Processor(IMemory memory)
		{
			Registers = new Registers();
			_memory = memory;
			_instructions = new IInstruction[256];
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
			SetupAndInstructions();
			SetupDecInstructions();
			SetupLdInstructions();
			SetupJpInstructions();
			SetupSubInstructions();
            SetupSbcInstructions();
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