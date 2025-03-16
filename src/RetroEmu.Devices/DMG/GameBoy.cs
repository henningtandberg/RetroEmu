using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;
using System;
using System.Runtime.Intrinsics.Arm;

namespace RetroEmu.Devices.DMG
{
	public class GameBoy : IGameBoy
	{
		private readonly ICartridge _cartridge;
		private readonly IMemory _memory;
		private readonly IProcessor _processor;
		private readonly IJoypad _joypad;

		public GameBoy(ICartridge cartridge, IMemory memory, IProcessor processor, IJoypad joypad)
		{
			_cartridge = cartridge;
			_memory = memory;
			_processor = processor;
			_joypad = joypad;
        }

        public string GetOutput()
		{
			return _memory.GetOutput();
		}

		public int GetCurrentClockSpeed()
		{
			return _processor.GetCurrentClockSpeed();
		}

		public void Reset()
		{
			_cartridge.Reset();
			_memory.Reset();
			_processor.Reset();
		}
		
		public void Load(byte[] cartridgeMemory)
		{
            // TODO: Get cartridge info and create the correct cartridge type depending on the info.
            CartridgeInfo cartridgeInfo = CartridgeInfo.Create(cartridgeMemory);

			// Have to somehow create a new cartridge and put it into memory
			// Maybe a _memory.setCartridge or something?
			switch (cartridgeInfo.CartridgeType)
			{
				case CartridgeType.RomOnly:
					// For now just load normally.
                    _cartridge.Load(cartridgeMemory);
					break;
				case CartridgeType.RomMbc1:
				case CartridgeType.RomMbc1RAM:
				case CartridgeType.RomMbc1RAMBattery:
                    // For now just load normally.
                    _cartridge.Load(cartridgeMemory);
					break;
				default:
					throw new NotImplementedException();
            }
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

		public CartridgeInfo GetCartridgeInfo()
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
		
		public IMemory GetMemory()
		{
			return _memory;
		}
	}
}