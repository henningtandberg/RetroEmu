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
		void RunAt(double framesPerSecond);
		
		void ButtonPressed(Button button);
		void ButtonReleased(Button button);
		void DPadPressed(DPad direction);
		void DPadReleased(DPad direction);
		
		CartridgeHeader GetCartridgeInfo();
		IProcessor GetProcessor();
		IAddressBus GetMemory();
	}
}