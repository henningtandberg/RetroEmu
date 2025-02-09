namespace RetroEmu.Devices.DMG.CPU;

public interface IProcessor
{
	public Registers Registers { get; }
	public void Reset();
	int Update();
	public void SetTimerSpeed(int speed);
	public void SetInterruptMasterEnable(bool value);
	public void SetInterruptEnable(InterruptType type, bool value);
	public void GenerateInterrupt(InterruptType type);
}