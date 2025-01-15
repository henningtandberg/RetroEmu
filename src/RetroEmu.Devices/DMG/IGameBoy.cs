using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public interface IGameBoy
	{
		public string GetOutput();
		public void Reset();
		void Load(byte[] rom);
		int Update();
		
		CartridgeInfo GetCartridgeInfo();
		IProcessor GetProcessor();
		IMemory GetMemory();
	}

	public enum RegisterName
	{
		A
	}
}