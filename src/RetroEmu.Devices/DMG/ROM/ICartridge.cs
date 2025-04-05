namespace RetroEmu.Devices.DMG.ROM;

public interface ICartridge
{
	CartridgeHeader GetCartridgeInfo();
	void Load(byte[] rom);
	
	public byte ReadROM(ushort address);
	public byte ReadRAM(ushort address);
	public void WriteForTests(ushort address, byte value);
	public void WriteROM(ushort address, byte value);
	public void WriteRAM(ushort address, byte value);
}