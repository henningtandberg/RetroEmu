namespace RetroEmu.Devices.DMG
{
	public class Processor : IProcessor
	{
		private readonly IMemory _memory;

		public Processor(ref IMemory memory)
		{
			_memory = memory;
		}

		public void Reset()
		{
		}
	}
}