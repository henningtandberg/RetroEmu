using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public interface IGameBoy
	{
		public string GetOutput();
		public int GetCurrentClockSpeed();
		public void Reset();
		void Load(byte[] rom);
		int Update();

		bool VBlankTriggered();
		void ButtonPressed(Button button);
		void ButtonReleased(Button button);
		void DPadPressed(DPad direction);
		void DPadReleased(DPad direction);
		
		CartridgeHeader GetCartridgeInfo();
		IProcessor GetProcessor();
		IAddressBus GetMemory();
	}
}