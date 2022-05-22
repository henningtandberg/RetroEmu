namespace RetroEmu.Devices.DMG.ROM
{
	public interface ICartridge
	{
		public void Reset();
		CartridgeInfo GetCartridgeInfo();
		void Load(byte[] rom);
	}
}