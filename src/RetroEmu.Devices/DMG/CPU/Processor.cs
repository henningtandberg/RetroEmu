namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
		private readonly IMemory _memory;
		private readonly delegate* managed<Processor, byte, byte>[] _instructions;

		public Registers Registers { get; }

		public Processor(IMemory memory)
		{
			Registers = new Registers();
			_memory = memory;
			_instructions = new delegate* managed<Processor, byte, byte>[256];
			SetUpInstructions();
		}

		private partial void SetUpInstructions();

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
	}
}