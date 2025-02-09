namespace RetroEmu.Devices.DMG.CPU;

public interface IProcessor
{
	public Registers Registers { get; }
	public void Reset();
	public int Update();
}