namespace RetroEmu.Devices.DMG.CPU;

public interface IProcessor
{
	public int GetCurrentClockSpeed();
	public void Reset();
	public int Update();
	public bool VBlankTriggered();
	public byte GetDisplayColor(int x, int y);
}