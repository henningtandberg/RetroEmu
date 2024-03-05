namespace RetroEmu.Devices.DMG
{
	public class Memory : IMemory
	{
		public void Reset()
		{
		}

		public byte Read(ushort address)
		{
			throw new System.NotImplementedException();
		}

        public void Write(ushort address, byte value)
        {
            throw new System.NotImplementedException();
        }
    }
}