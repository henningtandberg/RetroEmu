using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.PPU;

namespace RetroEmu.GB.TestSetup;

public interface ITestableProcessor : IProcessor
{
	public Registers GetRegisters();
	
    public bool GetValueOfCarryFlag();
    public bool GetValueOfHalfCarryFlag();
    public bool GetValueOfSubtractFlag();
    public bool GetValueOfZeroFlag();
	
    public void SetCarryFlagToValue(bool value);
    public void SetHalfCarryFlagToValue(bool value);
    public void SetSubtractFlagToValue(bool value);
    public void SetZeroFlagToValue(bool value);
	
    public void SetInterruptMasterEnableToValue(bool IME);
	
    public void SetSerialInterruptEnableToValue(bool IE);
    public void SetTimerInterruptEnableToValue(bool IE);
	
    public void GenerateSerialInterrupt();
    
	public void SetTimerCounter(byte newCounter);
	public void SetTimerModulo(byte newModulo);
	public void SetTimerControl(byte newControl);
    
	public IPixelProcessingUnit GetPixelProcessingUnit();
}