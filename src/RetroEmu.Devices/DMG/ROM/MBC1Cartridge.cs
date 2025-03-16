namespace RetroEmu.Devices.DMG.ROM;

public class MBC1Cartridge : ICartridge
{
	private CartridgeInfo _cartridgeInfo = new();
	private byte[] _rom = new byte[0x8000];

	public void Reset()
	{ }

	public CartridgeInfo GetCartridgeInfo()
	{
		return _cartridgeInfo;
	}

	public void Load(byte[] rom)
	{
		// To do: validate ROM
		_rom = rom;
		_cartridgeInfo = CartridgeInfo.Create(rom);
	}

	public byte ReadROM(ushort address)
	{
		// Need logic for selecting the correct ROM bank.
		return address < 0x8000 ? _rom[address] : (byte)0;
	}
	
	public byte ReadRAM(ushort address)
	{
		return 0;
	}

	public void WriteForTests(ushort address, byte value)
	{
		_rom[address] = value;
	}
	
	public void WriteROM(ushort address, byte value)
	{ 
		// Add rom bank switching logic here
	}
	
	public void WriteRAM(ushort address, byte value)
	{ }
}