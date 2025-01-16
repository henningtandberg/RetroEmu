using System;
using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.DMG
{
	public class Memory(ITimer timer) : IMemory
	{
		protected byte[] memory = new byte[0x10000];

		string output = "";

		public string GetOutput()
		{
			return output;
		}

		public void Reset()
		{
		}

		public byte Read(ushort address)
		{
			// TODO: Dersom det er en read på en av timer-registrene skal returnere veridene direkte fra timer-klassen

			if (address is < 0xFF08 and > 0xFF03)
			{
				return address switch {
					0xFF04 => timer.Divider,
					0xFF05 => timer.Counter,
					0xFF06 => timer.Modulo,
					0xFF07 => timer.Control,
					_ => memory[address]
				};
			}
			
			return memory[address];
		}

        public void Write(ushort address, byte value)
        {
			if (address == 0xFF02) // SC
			{
				if (value == 0x81)
                {
                    var letter = (char)memory[0xFF01]; // Get value from SB

                    output += letter;
                }
			}
			else if (address is < 0xFF08 and > 0xFF03)
			{
				switch (address)
				{
					case 0xFF04:
						timer.Divider = 0;
						break;
					case 0xFF05:
						timer.Counter = value;
						break;
					case 0xFF06:
						timer.Modulo = value;
						break;
					case 0xFF07:
						timer.Control = value;
						break;
				}
			}
			else
			{
				memory[address] = value;
			}
        }
        
        public void Load(byte[] rom)
		{
			Array.Copy(rom, 0, memory, 0, rom.Length);
		}
    }
}