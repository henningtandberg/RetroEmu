using System;
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
			SetupCbInstruction();
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
            SetupRestartInstructions();
            SetupScfInstruction();
		}

		public void Reset()
		{
		}

		public int Update()
		{
			var opcode = GetNextOpcode();
			var instr = _instructions[opcode];

			// Strangling old Instruction classes in favor of Instruction Record which will be passed to Execute
			return instr.GetType() == typeof(Instruction)
				? Execute(instr as Instruction) // New
				: instr.Execute(this);	// Old
		}

		private int Execute(Instruction instruction)
		{
			var (fetchCycles, fetchResult) = PerformFetchOperation(instruction.FetchType);
			var (opResult, opCycles) = PerformOperation(instruction.OpType, fetchResult);
			var writeCycles = PerformWriteOperation(instruction.WriteType, opResult);
			
			return fetchCycles + opCycles + writeCycles;
		}

		public byte GetNextOpcode()
		{
			var opcode = _memory.Read(*Registers.PC);
			(*Registers.PC)++;
			return opcode;
		}
	}
}