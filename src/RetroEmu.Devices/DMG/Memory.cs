using System;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using RetroEmu.Devices.DMG.CPU.Timing;

namespace RetroEmu.Devices.DMG
{
	public class Memory(ITimer timer, IPixelProcessingUnit pixelProcessingUnit, IInterruptState interruptState) : IMemory
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

	        if (address is >= 0x8000 and <= 0x9FFF)
	        {
		        return pixelProcessingUnit.ReadVRAM(address);
	        }
	        if (address is >= 0xFE00 and <= 0xFE9F)
	        {
		        return pixelProcessingUnit.ReadOAM(address);
	        }
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
            if (address == 0xFF0F)
			{
				return interruptState.InterruptFlag;
			}
            if (address == 0xFF40)
            {
				return pixelProcessingUnit.LCDC;
			}
            if (address == 0xFF41)
			{
	            return pixelProcessingUnit.STAT;
			}
            if (address == 0xFF44)
            {
	            return pixelProcessingUnit.LY;
            }
            if (address == 0xFF45)
            {
	            return pixelProcessingUnit.LYC;
            }
			if (address == 0xFFFF)
			{
				return interruptState.InterruptEnable;
			}

            return _memory[address];
		}

        public void Write(ushort address, byte value)
        {
	        if (address is >= 0x8000 and <= 0x9FFF)
	        {
		        pixelProcessingUnit.WriteVRAM(address, value);
	        }
	        if (address is >= 0xFE00 and <= 0xFE9F)
	        {
		        pixelProcessingUnit.WriteOAM(address, value);
	        }
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
            else if (address == 0xFF0F)
            {
                interruptState.InterruptFlag = value;
            }
	        else if (address == 0xFF40)
	        {
		        pixelProcessingUnit.LCDC = value;
	        }
	        else if (address == 0xFF41)
	        {
		        pixelProcessingUnit.STAT = value;
	        }
	        else if (address == 0xFF45)
	        {
		        pixelProcessingUnit.LYC = value;
	        }
            else if (address == 0xFFFF)
            {
                interruptState.InterruptEnable = value;
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