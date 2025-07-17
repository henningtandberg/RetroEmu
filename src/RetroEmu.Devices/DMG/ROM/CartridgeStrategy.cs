using System.Collections.Generic;

namespace RetroEmu.Devices.DMG.ROM;

public class CartridgeStrategy : ICartridge
{
    // TODO: Create ICartridgeStrategy and rename this class to Cartridge or CartridgeFacade or something
    private ICartridge _cartridge;

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
            0x01 or 0x02 or 0x03 => new MBC1Cartridge(),
            _ => throw new System.NotImplementedException()
        };
        
        _cartridge.Load(rom);
    }
    
    public CartridgeHeader GetCartridgeInfo() => _cartridge?.GetCartridgeInfo();

    public byte ReadROM(ushort address) => _cartridge?.ReadROM(address) ?? 0x00;

    public byte ReadRAM(ushort address) => _cartridge?.ReadRAM(address) ?? 0x00;

    public void WriteROM(ushort address, byte value) => _cartridge?.WriteROM(address, value);

    public void WriteRAM(ushort address, byte value) => _cartridge?.WriteRAM(address, value);

    #region DebugFeatures

    public List<byte[]> GetRomBanks() => _cartridge?.GetRomBanks() ?? [];

    #endregion
}