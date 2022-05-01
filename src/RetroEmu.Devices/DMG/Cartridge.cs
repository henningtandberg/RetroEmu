using System.Collections.Generic;

namespace RetroEmu.Devices.DMG
{
	public class Cartridge : ICartridge
	{
		private CartridgeInfo _cartridgeInfo = new();

		public void Reset()
		{
		}

		public CartridgeInfo GetCartridgeInfo()
		{
			return _cartridgeInfo;
		}

		public void Load(byte[] rom)
		{
			// To do: validate ROM
			_cartridgeInfo = CartridgeInfo.Create(rom);
		}
	}
}