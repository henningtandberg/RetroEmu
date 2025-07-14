namespace RetroEmu.Devices.DMG
{
	// TODO: Rename to MemoryBus og AddressBus
	public interface IMemory
	{
		public string GetOutput();

        public void Reset();
		byte Read(ushort address);
		void Write(ushort address, byte value);
		void Load(byte[] rom);
	}
}