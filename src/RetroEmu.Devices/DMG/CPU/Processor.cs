using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public unsafe partial class Processor(IMemory memory) : IProcessor
{
    private readonly Instruction[] _instructions = InstructionTableFactory.Create();

    public Registers Registers { get; } = new();
    public int Cycles { get; set; }

    public InterruptState InterruptState { get; set; } = new();

    public void GenerateInterrupt(InterruptType type)
    {
        // Step 1 of interrupt procedure "When an interrupt is generated, the IF flag will be set"
        InterruptState.InterruptFlag |= (byte)type;
    }

    private void HandleInterrupts()
    {
        if (InterruptState.InterruptMasterEnable)
        {
            // Iterate through IF by priority
            InterruptType[] interruptsByPriority = [InterruptType.VBlank, InterruptType.LCDC, InterruptType.Timer, InterruptType.Serial, InterruptType.Button];
            byte selectedInterrupt = 0;
            foreach (InterruptType interrupt in interruptsByPriority)
            {
                // If interrupt is enabled and triggered
                if (((InterruptState.InterruptEnable & (byte)interrupt) != 0) && ((InterruptState.InterruptFlag & (byte)interrupt) != 0))
                {
                    selectedInterrupt = (byte)interrupt;
                    break;
                }
            }

            if (selectedInterrupt != 0) 
            {
                // Step 3 of interrupt procedure, reset IME
                InterruptState.InterruptMasterEnable = false;

                // Step 4 of interrupt procedure, push PC to stack
                Push16ToStack(*Registers.PC);

                // Step 5 of interrupt procedure, jump to starting address of the interrupt
                *Registers.PC = InterruptState.GetInterruptStartingAddress((InterruptType)selectedInterrupt);

                // Reset the IF register
                InterruptState.InterruptFlag &= (byte)(~selectedInterrupt);
            }
        }
    }

    public void Reset()
    {
    }

    public int Update()
    {
        HandleInterrupts();

        var opcode = GetNextOpcode();
        var instr = _instructions[opcode];

        return instr.OpType == OpType.PreCb
            ? ExecuteCbInstruction()
            : ExecuteInstruction(instr);
    }

    private int ExecuteInstruction(Instruction instruction)
    {
        var (fetchCycles, fetchResult) = PerformFetchOperation(instruction.FetchType);
        var (opResult, opCycles) = PerformOperation(instruction.OpType, fetchResult);
        var writeCycles = PerformWriteOperation(instruction.WriteType, opResult);

        return fetchCycles + opCycles + writeCycles;
    }

    private int ExecuteCbInstruction()
    {
        var cbOpCode = GetNextOpcode();
        var cbType = DecodeCbType(cbOpCode);
        var fetchType = DecodeFetchType(cbOpCode);
        var writeType = DecodeWriteType(cbOpCode);
        var (fetchCycles, fetchResult) = PerformFetchOperation(fetchType);
        var (opCycles, opResult) = PerformCbOperation(cbType, fetchResult);
        var writeCycles = PerformWriteOperation(writeType, opResult);

        const int cbCycles = 4;
        return cbCycles + fetchCycles + opCycles + writeCycles;
    }

    private byte GetNextOpcode()
    {
        var opcode = memory.Read(*Registers.PC);
        (*Registers.PC)++;
        return opcode;
    }
}