namespace RetroEmu.Devices.DMG
{
	public interface IMemory
	{
		public void Reset();
		byte Get(ushort address);
	}
}