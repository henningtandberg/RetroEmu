namespace RetroEmu.Devices.DMG
{
	public interface IMemory
	{
		public string GetOutput();

        public void Reset();
		byte Read(ushort address);
		void Write(ushort address, byte value);
		void Load(byte[] rom);
		byte[] GetMemory();
	}
}