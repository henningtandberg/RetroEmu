using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.ROM;

namespace RetroEmu.Devices.DMG
{
	public class GameBoy : IGameBoy
	{
		private readonly ICartridge _cartridge;
		private readonly IMemory _memory;
		private readonly IProcessor _processor;

		public GameBoy(ICartridge cartridge, IMemory memory, IProcessor processor)
		{
			_cartridge = cartridge;
			_memory = memory;
			_processor = processor;
        }

        public string GetOutput()
		{
			return _memory.GetOutput();
		}

        public void Reset()
		{
			_cartridge.Reset();
			_memory.Reset();
			_processor.Reset();
		}
		
		public void Load(byte[] rom)
		{
			_cartridge.Load(rom);
			_memory.Load(rom);
		}

		public CartridgeInfo GetCartridgeInfo()
		{
			return _cartridge.GetCartridgeInfo();
		}

		public int Update()
		{
			return _processor.Update();
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