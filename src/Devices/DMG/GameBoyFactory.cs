namespace RetroEmu.Devices.DMG
{
	public static class GameBoyFactory
	{
		public static GameBoy CreateGameBoy()
		{
			ICartridge cartridge = new Cartridge();
			IMemory memory = new Memory();
			IProcessor processor = new Processor(ref memory);

			return new GameBoy(
				cartridge,
				memory,
				processor
			);
		}
	}
}