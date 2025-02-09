using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.Tests.Setup;

public interface ITestableProcessor : IProcessor
{
    public void SetCarryFlag();
    public void SetHalfCarryFlag();
    public void SetSubtractFlag();
    public void SetZeroFlag();
	
    public bool CarryFlagIsSet();
    public bool HalfCarryFlagIsSet();
    public bool SubtractFlagIsSet();
    public bool ZeroFlagIsSet();
	
    public void ClearCarryFlag();
    public void ClearHalfCarryFlag();
    public void ClearSubtractFlag();
    public void ClearZeroFlag();
	
    public void SetCarryFlagToValue(bool value);
    public void SetHalfCarryFlagToValue(bool value);
    public void SetSubtractFlagToValue(bool value);
    public void SetZeroFlagToValue(bool value);
	
    public void SetInterruptMasterEnableToValue(bool IME);
	
    public void SetSerialInterruptEnableToValue(bool IE);
    public void SetTimerInterruptEnableToValue(bool IE);
	
    public void GenerateSerialInterrupt();
}