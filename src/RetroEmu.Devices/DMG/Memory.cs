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
			if (address == 0xFF00)
			{
				return 0x00; // P1 - Joypad
			}
			if (address == 0xFF01)
			{
                return 0x00; // SB - Serial transfer
            }
            if (address == 0xFF02)
            {
                return 0x00; // SC - Serial control
            }
            if (address == 0xFF03)
            {
                return 0x00; // Unused
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
            if (address is < 0xFF0F and >= 0xFF08)
            {
                return 0x00; // Unused
            }
            if (address == 0xFF0F)
			{
				return interruptState.InterruptFlag;
			}
            if (address is < 0xFF26 and > 0xFF0F)
            {
                return 0x00; // Sound registers
            }
            if (address is < 0xFF30 and >= 0xFF26)
            {
                return 0x00; // Unused
            }
            if (address is < 0xFF40 and >= 0xFF30)
            {
                return 0x00; // Sound RAM
            }
            if (address == 0xFF40)
            {
				return pixelProcessingUnit.LCDC;
			}
            if (address == 0xFF41)
			{
	            return pixelProcessingUnit.STAT;
			}
			if (address == 0xFF42)
			{
				return pixelProcessingUnit.SCY;
			}
			if (address == 0xFF43)
			{
				return pixelProcessingUnit.SCX;
			}
            if (address == 0xFF44)
            {
	            return pixelProcessingUnit.LY;
            }
            if (address == 0xFF45)
            {
	            return pixelProcessingUnit.LYC;
            }
            if (address == 0xFF46)
            {
                return 0x00; // OAM DMA
            }
            if (address == 0xFF47)
            {
                return 0x00; // BGP - BG Palette
            }
            if (address == 0xFF48)
            {
                return 0x00; // OBP0 - Object Palette 0
            }
            if (address == 0xFF49)
            {
                return 0x00; // OBP1 - Object Palette 1
            }
            if (address == 0xFF4A)
			{
				return pixelProcessingUnit.WY;
			}
			if (address == 0xFF4B)
			{
				return pixelProcessingUnit.WX;
			}
            if (address is < 0xFF80 and > 0xFF4B)
            {
                return 0x00; // Unused
            }
            if (address is < 0xFFFF and >= 0xFF80)
            {
                return _memory[address]; // HRAM - High RAM
            }
            if (address == 0xFFFF)
			{
				return interruptState.InterruptEnable;
			}

            return _memory[address];
		}

        public void Write(ushort address, byte value)
        {
	        // Not allowed to write to ROM
	        if (address <= 0x7FFF)
	        {
		        return;
	        }
	        
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
			if (address == 0xFF42)
			{
				pixelProcessingUnit.SCY = value;
			}
			if (address == 0xFF43)
			{
				pixelProcessingUnit.SCX = value;
			}
	        else if (address == 0xFF45)
	        {
		        pixelProcessingUnit.LYC = value;
	        }
			if (address == 0xFF4A)
			{
				pixelProcessingUnit.WY = value;
			}
			if (address == 0xFF4B)
			{
				pixelProcessingUnit.WX = value;
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