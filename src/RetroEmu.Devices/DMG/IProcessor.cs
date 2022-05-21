namespace RetroEmu.Devices.DMG
{
	public interface IProcessor
	{
		public Registers Registers { get; }
		public void Reset();
		int Update();
	}
}