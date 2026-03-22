using RetroEmu.Devices.GameBoy.CPU;
using RetroEmu.Devices.GameBoy.ROM;

namespace RetroEmu.Devices.GameBoy
{
	public interface IGameBoy
	{
		public string GetOutput();
		public void Reset();
		void Load(byte[] rom);
		int Update();
		void RunAt(double frameRate);
		
		void ButtonPressed(Button button);
		void ButtonReleased(Button button);
		void DPadPressed(DPad direction);
		void DPadReleased(DPad direction);
		
		CartridgeHeader GetCartridgeInfo();
		IAddressBus GetMemory();
	}
}