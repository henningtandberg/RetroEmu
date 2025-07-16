using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public class GameBoy : IGameBoy
	{
		private readonly ICartridge _cartridge;
		private readonly IAddressBus _addressBus;
		private readonly IProcessor _processor;
		private readonly IJoypad _joypad;

		public GameBoy(ICartridge cartridge, IAddressBus addressBus, IProcessor processor, IJoypad joypad)
		{
			_cartridge = cartridge;
			_addressBus = addressBus;
			_processor = processor;
			_joypad = joypad;
        }

        public string GetOutput()
		{
			return _addressBus.GetOutput();
		}

		public int GetCurrentClockSpeed()
		{
			return _processor.GetCurrentClockSpeed();
		}

		public void Reset()
		{
			_addressBus.Reset();
			_processor.Reset();
		}
		
		public void Load(byte[] cartridgeMemory)
		{
			_cartridge.Load(cartridgeMemory);
			_processor.Reset();
		}

		public void ButtonPressed(Button button)
		{
			_joypad.PressButton((byte)button);
		}

		public void ButtonReleased(Button button)
		{
			_joypad.ReleaseButton((byte)button);
		}
		
		public void DPadPressed(DPad direction)
		{
			_joypad.PressDPad((byte)direction);
		}

		public void DPadReleased(DPad direction)
		{
			_joypad.ReleaseDPad((byte)direction);
		}

		public CartridgeHeader GetCartridgeInfo()
		{
			return _cartridge.GetCartridgeInfo();
		}

		public int Update()
		{
			return _processor.Update();
		}

		public bool VBlankTriggered()
		{
			return _processor.VBlankTriggered();
		}

		public IProcessor GetProcessor()
		{
			return _processor;
		}
		
		public IAddressBus GetMemory()
		{
			return _addressBus;
		}
	}
}