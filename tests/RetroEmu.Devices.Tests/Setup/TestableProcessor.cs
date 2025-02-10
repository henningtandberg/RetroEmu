using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.Tests.Setup;

public class TestableProcessor(IMemory memory, ITimer timer, IInterruptState interruptState)
    : Processor(memory, timer, interruptState), ITestableProcessor
{
	public Registers GetRegisters() => Registers;
	
	public void SetCarryFlag() => SetFlag(Flag.Carry);
	public void SetHalfCarryFlag() => SetFlag(Flag.HalfCarry);
	public void SetSubtractFlag() => SetFlag(Flag.Subtract);
	public void SetZeroFlag() => SetFlag(Flag.Zero);

	public bool CarryFlagIsSet() => IsSet(Flag.Carry);
	public bool HalfCarryFlagIsSet() => IsSet(Flag.HalfCarry); 
	public bool SubtractFlagIsSet() => IsSet(Flag.Subtract);
	public bool ZeroFlagIsSet() => IsSet(Flag.Zero);

	public void ClearCarryFlag() => ClearFlag(Flag.Carry);
	public void ClearHalfCarryFlag() => ClearFlag(Flag.HalfCarry);
	public void ClearSubtractFlag() => ClearFlag(Flag.Subtract);
	public void ClearZeroFlag() => ClearFlag(Flag.Zero);

	public void SetCarryFlagToValue(bool value) => SetFlagToValue(Flag.Carry, value);
	public void SetHalfCarryFlagToValue(bool value) => SetFlagToValue(Flag.HalfCarry, value);
	public void SetSubtractFlagToValue(bool value) => SetFlagToValue(Flag.Subtract, value);
	public void SetZeroFlagToValue(bool value) => SetFlagToValue(Flag.Zero, value);

	public void SetInterruptMasterEnableToValue(bool IME) => SetInterruptMasterEnable(IME);
	
	public void SetSerialInterruptEnableToValue(bool IE) => SetInterruptEnable(InterruptType.Serial, IE);
	public void SetTimerInterruptEnableToValue(bool IE) => SetInterruptEnable(InterruptType.Timer, IE);
	
	public void GenerateSerialInterrupt() => GenerateInterrupt(InterruptType.Serial);
    public void SetTimerSpeed(int speed) => timer.SetSpeed(speed);
}