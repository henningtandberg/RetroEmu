namespace RetroEmu.Devices.DMG
{
	public interface IMemory
	{
		public void Reset();
		byte Read(ushort address);
		void Write(ushort address, byte value);
	}
}