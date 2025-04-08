namespace RetroEmu.Devices.DMG.ROM;

public class CartridgeStrategy : ICartridge
{
    private ICartridge _cartridge = new NoMBCCartridge();

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void Load(byte[] rom)
    {
        var cartridgeType = rom[0x0147];

        _cartridge = cartridgeType switch
        {
            0x00 => new NoMBCCartridge(),
            _ => throw new System.NotImplementedException()
        };
        
        _cartridge.Load(rom);
    }
    
    public CartridgeHeader GetCartridgeInfo() => _cartridge.GetCartridgeInfo();

    public byte ReadROM(ushort address) => _cartridge.ReadROM(address);

    public byte ReadRAM(ushort address) => _cartridge.ReadRAM(address);

    public void WriteROM(ushort address, byte value) => _cartridge.WriteROM(address, value);

    public void WriteRAM(ushort address, byte value) => _cartridge.WriteRAM(address, value);
}