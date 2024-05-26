using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
		private readonly IMemory _memory;
		private readonly IInstruction[] _instructions;

		public Registers Registers { get; }
		public int Cycles { get; set; }

		public Processor(IMemory memory)
		{
			Registers = new Registers();
			_memory = memory;
			_instructions = new IInstruction[256];
			SetUpInstructions();
		}

		private void SetUpInstructions()
		{
			SetupNopInstruction();
			SetupAddInstructions();
			SetupAdcInstructions();
			SetupAndInstructions();
			SetupDecInstructions();
			SetupIncInstructions();
			SetupLdInstructions();
			SetupJpInstructions();
			SetupOrInstructions();
			SetupSubInstructions();
            SetupSbcInstructions();
            SetupRotateInstructions();
            SetupCpInstructions();
            SetupOrInstructions();
            SetupXorInstructions();
            SetupCallInstructions();
            SetupRetInstructions();
            SetupPopInstructions();
            SetupPushInstructions();
            SetupCplInstruction();
            SetupCcfInstruction();
            SetupDaaInstruction();
		}

		public void Reset()
		{
		}

		public int Update()
		{
			var opcode = GetNextOpcode();
			var instr = _instructions[opcode];
			return instr.Execute(this);
		}

		public byte GetNextOpcode()
		{
			var opcode = _memory.Read(*Registers.PC);
			(*Registers.PC)++;
			return opcode;
		}
	}
}