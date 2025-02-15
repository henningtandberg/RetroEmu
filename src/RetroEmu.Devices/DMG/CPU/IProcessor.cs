namespace RetroEmu.Devices.DMG.CPU;

public interface IProcessor
{
	public int GetCurrentClockSpeed();
	public void Reset();
	public int Update();
}