using System;

namespace RetroEmu.Devices.DMG.ROM;

public class NoMBCCartridge : ICartridge
{
	private CartridgeHeader _cartridgeHeader;
	private byte[] _cartridgeRom;

	public CartridgeHeader GetCartridgeInfo() => _cartridgeHeader;

	public void Load(byte[] rom)
	{
		_cartridgeHeader = CartridgeHeaderBuilder
			.Create(rom)
			.Build();

		_cartridgeRom = new byte[_cartridgeHeader.RomSizeInfo.SizeBytes];
		Buffer.BlockCopy(rom, 0, _cartridgeRom, 0, _cartridgeRom.Length);
	}

	public byte ReadROM(ushort address)
	{
		return address < 0x8000 ? _cartridgeRom[address] : (byte)0;
	}
	
	public byte ReadRAM(ushort address)
	{
		return 0;
	}

	public void WriteForTests(ushort address, byte value)
	{
		_cartridgeRom[address] = value;
	}
	
	public void WriteROM(ushort address, byte value)
	{ }
	
	public void WriteRAM(ushort address, byte value)
	{ }

	public void Reset()
	{
		throw new NotImplementedException();
	}
}