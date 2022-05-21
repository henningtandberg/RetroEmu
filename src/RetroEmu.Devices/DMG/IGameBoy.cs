namespace RetroEmu.Devices.DMG
{
	public interface IGameBoy
	{
		public void Reset();
		void Load(byte[] rom);
		CartridgeInfo GetCartridgeInfo();
		int Update();
		IProcessor GetProcessor();
	}

	public enum RegisterName
	{
		A
	}
}