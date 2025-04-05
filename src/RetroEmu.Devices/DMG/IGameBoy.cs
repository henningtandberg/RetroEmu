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
		IMemory GetMemory();
	}

	public enum Button : byte
	{
		A = 0,
		B = 1,
		Select = 2,
		Start = 3
	}
	
	public enum DPad : byte
	{
		Right = 0,
		Left = 1,
		Up = 2,
		Down = 3
	}
}