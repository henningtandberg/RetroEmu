namespace RetroEmu.Devices.DMG
{
	public class GameBoy : IGameBoy
	{
		private readonly ICartridge _cartridge;
		private readonly IMemory _memory;

		public GameBoy(ICartridge cartridge, IMemory memory, IProcessor processor)
		{
			_cartridge = cartridge;
			_memory = memory;
		}

		public void Reset()
		{
			_cartridge.Reset();
		}
	}
}