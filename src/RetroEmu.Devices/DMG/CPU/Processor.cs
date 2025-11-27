using System;
using RetroEmu.Devices.DMG.CPU.Instructions;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.Link;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.Devices.DMG.CPU;

public partial class Processor(
    IAddressBus addressBus,
    ITimer timer,
    IPixelProcessingUnit pixelProcessingUnit,
    IInterruptState interruptState,
    ISerial serial,
    IJoypad joypad)
    : IProcessor, IDebugProcessor
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
        timer.Update(cycles);
        pixelProcessingUnit.Update(cycles);
        serial.Update(cycles);
        joypad.Update();
            
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
        var opcode = addressBus.Read(Registers.PC);
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
        var cbType = cbOpCode.DecodeCbType();
        var fetchType = cbOpCode.DecodeFetchType();
        var writeType = cbOpCode.DecodeWriteType();
        var (fetchCycles, fetchResult) = PerformFetchOperation(fetchType);
        var (opResult, opCycles) = PerformCbOperation(cbType, fetchResult);
        var writeCycles = PerformWriteOperation(writeType, opResult);

        const int cbCycles = 4;
        return cbCycles + fetchCycles + opCycles + writeCycles;
    }

    public bool VBlankTriggered()
    {
        return pixelProcessingUnit.VBlankTriggered();
    }
    public byte GetDisplayColor(int x, int y)
    {
        return pixelProcessingUnit.ReadPixelMemory(x, y);
    }
    
    /// <summary>
    /// The following region contains methods required by IDebugProcessor,
    /// and are exposed for debugging purposes only
    /// </summary>
    #region DebugFeatures
    
    public Registers GetRegisters() => Registers;
    
    public bool CarryFlagIsSet() => IsSet(Flag.Carry);
    public bool HalfCarryFlagIsSet() => IsSet(Flag.HalfCarry); 
    public bool SubtractFlagIsSet() => IsSet(Flag.Subtract);
    public bool ZeroFlagIsSet() => IsSet(Flag.Zero);

    #endregion
}