using RetroEmu.Devices.DMG.CPU.Instructions;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor(IMemory memory, ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState) : IProcessor
{
    private readonly Instruction[] _instructions = InstructionTableFactory.Create();
    protected Registers Registers { get; } = new();

    // Make this part of a new interface for a debuggable processor
    public int GetCurrentClockSpeed()
    {
        // 4.194304 MHz
        return 4194304;
    }

    public void Reset()
    {
        // TODO: Reset other dependencies also
        Registers.PC = 0x0100;
    }

    public int Update()
    {
        HandleInterrupts();

        var opcode = GetNextOpcode();
        var instr = _instructions[opcode];

        var cycles = instr.OpType == OpType.PreCb
            ? ExecuteCbInstruction()
            : ExecuteInstruction(instr);

        interruptState.Update();

        if (timer.Update(cycles))
        {
            interruptState.GenerateInterrupt(InterruptType.Timer);
        }
        
        pixelProcessingUnit.Update(cycles);
            
        return cycles;
    }

    private void HandleInterrupts()
    {
        if (!interruptState.IsInterruptMasterEnabled())
            return;

        var selectedInterrupt = interruptState.GetSelectedInterrupt();
        if (selectedInterrupt == 0)
            return;
        
        // Step 3 of interrupt procedure, reset IME
        interruptState.SetInterruptMasterEnable(false);

        // Step 4 of interrupt procedure, push PC to stack
        Push16ToStack(Registers.PC);

        // Step 5 of interrupt procedure, jump to starting address of the interrupt
        Registers.PC = interruptState.GetInterruptStartingAddress((InterruptType)selectedInterrupt);

        // Reset the IF register
        interruptState.ResetInterruptFlag(selectedInterrupt);
    }

    private byte GetNextOpcode()
    {
        var opcode = memory.Read(Registers.PC);
        Registers.PC++;
        return opcode;
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
}