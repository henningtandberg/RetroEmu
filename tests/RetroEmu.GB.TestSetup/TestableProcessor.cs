using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.Link;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.GB.TestSetup;

public class TestableProcessor(IAddressBus addressBus, ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState, ISerial serial, IJoypad joypad)
    : Processor(addressBus, timer, pixelProcessingUnit, interruptState, serial, joypad), ITestableProcessor
{
	public Registers GetRegisters() => Registers;
	
	public void SetCarryFlag() => SetFlag(Flag.Carry);
	public void SetHalfCarryFlag() => SetFlag(Flag.HalfCarry);
	public void SetSubtractFlag() => SetFlag(Flag.Subtract);
	public void SetZeroFlag() => SetFlag(Flag.Zero);

	public bool GetValueOfCarryFlag() => IsSet(Flag.Carry);
	public bool GetValueOfHalfCarryFlag() => IsSet(Flag.HalfCarry); 
	public bool GetValueOfSubtractFlag() => IsSet(Flag.Subtract);
	public bool GetValueOfZeroFlag() => IsSet(Flag.Zero);

	public void ClearCarryFlag() => ClearFlag(Flag.Carry);
	public void ClearHalfCarryFlag() => ClearFlag(Flag.HalfCarry);
	public void ClearSubtractFlag() => ClearFlag(Flag.Subtract);
	public void ClearZeroFlag() => ClearFlag(Flag.Zero);

	public void SetCarryFlagToValue(bool value) => SetFlagToValue(Flag.Carry, value);
	public void SetHalfCarryFlagToValue(bool value) => SetFlagToValue(Flag.HalfCarry, value);
	public void SetSubtractFlagToValue(bool value) => SetFlagToValue(Flag.Subtract, value);
	public void SetZeroFlagToValue(bool value) => SetFlagToValue(Flag.Zero, value);

	public void SetInterruptMasterEnableToValue(bool IME) => interruptState.SetInterruptMasterEnable(IME);
	
	public void SetSerialInterruptEnableToValue(bool IE) => interruptState.SetInterruptEnable(InterruptType.Serial, IE);
	public void SetTimerInterruptEnableToValue(bool IE) => interruptState.SetInterruptEnable(InterruptType.Timer, IE);
	public void SetJoypadInterruptEnableToValue(bool value) => interruptState.SetInterruptEnable(InterruptType.Button, value);
	public void GenerateSerialInterrupt() => interruptState.GenerateInterrupt(InterruptType.Serial);
	
	public void SetTimerCounter(byte newCounter) => timer.Counter = newCounter;
	public void SetTimerModulo(byte newModulo) => timer.Modulo = newModulo;
	public void SetTimerControl(byte newControl) => timer.Control = newControl;
	
    public IPixelProcessingUnit GetPixelProcessingUnit() => pixelProcessingUnit;

}