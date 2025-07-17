namespace RetroEmu.Devices.DMG.ROM;

public interface ICartridge : IDebugCartridge
{
	CartridgeHeader GetCartridgeInfo();
	void Load(byte[] rom);
	
	public byte ReadROM(ushort address);
	public void WriteROM(ushort address, byte value);
	
	public byte ReadRAM(ushort address);
	public void WriteRAM(ushort address, byte value);
}