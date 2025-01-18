using System;
using RetroEmu.Devices.DMG.CPU;

namespace RetroEmu.Devices.DMG
{
	public class Memory(ITimer timer) : IMemory
	{
		private readonly byte[] _memory = new byte[0x10000];

		string output = "";

		public string GetOutput()
		{
			return output;
		}

		public void Reset()
		{
			Array.Clear(_memory, 0, _memory.Length);
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
					_ => _memory[address]
				};
			}
			
			return _memory[address];
		}

        public void Write(ushort address, byte value)
        {
			if (address == 0xFF02) // SC
			{
				if (value == 0x81)
                {
                    var letter = (char)_memory[0xFF01]; // Get value from SB

                    output += letter;
                    Console.Write(letter);
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
				_memory[address] = value;
			}
        }
        
        public void Load(byte[] rom)
		{
			Array.Copy(rom, 0, _memory, 0, rom.Length);
		}
    }
}