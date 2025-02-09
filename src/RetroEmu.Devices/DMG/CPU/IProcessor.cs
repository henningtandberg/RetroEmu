namespace RetroEmu.Devices.DMG.CPU;

public interface IProcessor
{
	public Registers Registers { get; }
	public void Reset();
	int Update();
	public void SetTimerSpeed(int speed);
}