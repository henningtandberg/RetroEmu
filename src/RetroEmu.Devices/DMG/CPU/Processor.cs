using RetroEmu.Devices.DMG.CPU.Instructions;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor(IMemory memory, ITimer timer, IInterruptState interruptState) : IProcessor
{
    private readonly Instruction[] _instructions = InstructionTableFactory.Create();
    public Registers Registers { get; } = new();
    
    public void SetInterruptMasterEnable(bool value)
    {
        interruptState.InterruptMasterEnable = value;
    }
    public void SetInterruptEnable(InterruptType type, bool value)
    {
        if (value)
        {
            interruptState.InterruptEnable |= (byte)type;
        }
        else
        {
            interruptState.InterruptEnable &= (byte)~(byte)type;
        }
    }
    public void GenerateInterrupt(InterruptType type)
    {
        // Step 1 of interrupt procedure "When an interrupt is generated, the IF flag will be set"
        interruptState.InterruptFlag |= (byte)type;
    }

    private void HandleInterrupts()
    {
        if (interruptState.InterruptMasterEnable)
        {
            // Iterate through IF by priority
            InterruptType[] interruptsByPriority = [InterruptType.VBlank, InterruptType.LCDC, InterruptType.Timer, InterruptType.Serial, InterruptType.Button];
            byte selectedInterrupt = 0;
            foreach (InterruptType interrupt in interruptsByPriority)
            {
                // If interrupt is enabled and triggered
                if ((interruptState.InterruptEnable & (byte)interrupt) != 0 && (interruptState.InterruptFlag & (byte)interrupt) != 0)
                {
                    selectedInterrupt = (byte)interrupt;
                    break;
                }
            }

            if (selectedInterrupt != 0) 
            {
                // Step 3 of interrupt procedure, reset IME
                interruptState.InterruptMasterEnable = false;

                // Step 4 of interrupt procedure, push PC to stack
                Push16ToStack(Registers.PC);

                // Step 5 of interrupt procedure, jump to starting address of the interrupt
                Registers.PC = interruptState.GetInterruptStartingAddress((InterruptType)selectedInterrupt);

                // Reset the IF register
                interruptState.InterruptFlag &= (byte)~selectedInterrupt;
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

        var cycles = instr.OpType == OpType.PreCb
            ? ExecuteCbInstruction()
            : ExecuteInstruction(instr);

        if (interruptState.DisableInterruptCounter == 1)
        {
            interruptState.DisableInterruptCounter = 0;
            interruptState.InterruptMasterEnable = false;
        }
        else if (interruptState.DisableInterruptCounter > 1)
        {
            interruptState.DisableInterruptCounter--;
        }
        if (interruptState.EnableInterruptCounter == 1)
        {
            interruptState.EnableInterruptCounter = 0;
            interruptState.InterruptMasterEnable = true;
        }
        else if (interruptState.EnableInterruptCounter > 1)
        {
            interruptState.EnableInterruptCounter--;
        }

        if (timer.Update(cycles))
        {
            GenerateInterrupt(InterruptType.Timer);
        }
            
        return cycles;
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
        var (opResult, opCycles) = PerformCbOperation(cbType, fetchResult);
        var writeCycles = PerformWriteOperation(writeType, opResult);

        const int cbCycles = 4;
        return cbCycles + fetchCycles + opCycles + writeCycles;
    }

    private byte GetNextOpcode()
    {
        var opcode = memory.Read(Registers.PC);
        Registers.PC++;
        return opcode;
    }
}