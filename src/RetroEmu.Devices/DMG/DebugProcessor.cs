using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.Devices.DMG;

public class DebugProcessor(IAddressBus addressBus, ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState, IJoypad joypad)
    : Processor(addressBus, timer, pixelProcessingUnit, interruptState, joypad), IDebugProcessor
{
    public Registers GetRegisters() => Registers;
    
    public bool CarryFlagIsSet() => IsSet(Flag.Carry);
    public bool HalfCarryFlagIsSet() => IsSet(Flag.HalfCarry); 
    public bool SubtractFlagIsSet() => IsSet(Flag.Subtract);
    public bool ZeroFlagIsSet() => IsSet(Flag.Zero);
}