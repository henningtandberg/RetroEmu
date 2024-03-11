using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU
{
	public unsafe partial class Processor : IProcessor
	{
		private readonly IMemory _memory;
		private readonly IInstruction[] _instructions;
        private readonly delegate* managed<Processor, IOperationInput, IOperationOutput>[] _ops;
        private readonly delegate* managed<Processor, ushort, byte>[] _writeOps;

		public Registers Registers { get; }

		public Processor(IMemory memory)
		{
			Registers = new Registers();
			_memory = memory;
			_instructions = new IInstruction[256];
            _ops = new delegate* managed<Processor, IOperationInput, IOperationOutput>[EnumImplementation.Size<OpType>()];
            _writeOps = new delegate* managed<Processor, ushort, byte>[EnumImplementation.Size<WriteType>()];
			SetUpInstructions();
		}

		private void SetUpInstructions()
		{
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
			return instr.Execute(this, _ops, _writeOps);
		}

		private byte GetNextOpcode()
		{
			var opcode = _memory.Read(*Registers.PC);
			(*Registers.PC)++;
			return opcode;
		}
	}

	public interface IOperationOutput
	{
		public ushort Value { get; init; }
		public byte Cycles { get; init; }
	}

	public interface IOperationInput
	{
		public ushort Value { get; }
	}
	
	public record OperationInput(ushort Value) : IOperationInput;
	public record OperationOutput(ushort Value, byte Cycles) : IOperationOutput;
	public record JumpOperationInput(ushort Value, bool ConditionIsMet) : IOperationInput;
}