namespace RetroEmu.Devices.DMG
{
	public interface ICartridge
	{
		public void Reset();
		CartridgeInfo GetCartridgeInfo();
		void Load(byte[] rom);
	}
}