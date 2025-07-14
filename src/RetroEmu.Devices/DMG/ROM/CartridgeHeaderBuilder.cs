using System;
using System.Text;

namespace RetroEmu.Devices.DMG.ROM;

public class CartridgeHeaderBuilder
{
    private const ushort CartridgeHeaderStart = 0x0100;
    private const ushort CartridgeHeaderEnd = 0x014F;
    private const ushort CartridgeHeaderSize = 0x0150;
    
    private const ushort GameTitleStart = 0x0134 - CartridgeHeaderStart;
    private const ushort GameTitleEnd = 0x0142 - CartridgeHeaderStart;
    private const ushort ColorFlag = 0x0143 - CartridgeHeaderStart;
    private const ushort SuperGameBoyFlag = 0x0146 - CartridgeHeaderStart;
    private const ushort CartridgeTypeFlag = 0x0147 - CartridgeHeaderStart;
    private const ushort RomSizeFlag = 0x0148 - CartridgeHeaderStart;
    private const ushort RamSizeFlag = 0x0149 - CartridgeHeaderStart;
    private const ushort DestinationCodeFlag = 0x014A - CartridgeHeaderStart;
    private const ushort LicenseCodeFlag = 0x014B - CartridgeHeaderStart;
    
    private const uint KiloByte = 1024;
    
    private readonly byte[] _cartridgeHeaderMemory = new byte[CartridgeHeaderSize];

    private CartridgeHeaderBuilder(byte[] rom)
    {
        var cartridgeHeaderMemory = rom[CartridgeHeaderStart..CartridgeHeaderEnd];
        Buffer.BlockCopy(cartridgeHeaderMemory, 0, _cartridgeHeaderMemory, 0, cartridgeHeaderMemory.Length);
    }
    
    public static CartridgeHeaderBuilder Create(byte[] rom) =>
        new(rom);

    public CartridgeHeader Build() => new(
        GameTitle: GetGameTitle(),
        HasColor: HasColor(),
        HasGameBoySuperFunctions: HasSuperGameBoyFunctions(),
        CartridgeType: GetCartridgeType(),
        RomSizeInfo: GetRomSizeInfo(),
        RamSizeInfo: GetRamSizeInfo(),
        DestinationCode: GetDestinationCode(),
        LicenseCode: GetLicenseCode());

    private string GetGameTitle()
    {
        var gameTitleLocation = GameTitleStart..GameTitleEnd;
        var gameTitleMemory = _cartridgeHeaderMemory[gameTitleLocation];
        
        return Encoding.ASCII
            .GetString(gameTitleMemory)
            .TrimEnd('\0')
            .Trim();
    }

    private bool HasColor() => _cartridgeHeaderMemory[ColorFlag] == 0x80;

    private bool HasSuperGameBoyFunctions() => _cartridgeHeaderMemory[SuperGameBoyFlag] == 0x03;

    private CartridgeType GetCartridgeType() => _cartridgeHeaderMemory[CartridgeTypeFlag] switch
    {
        0x00 => CartridgeType.ROMOnly,
        0x01 => CartridgeType.ROMMBC1,
        0x02 => CartridgeType.ROMMBC1RAM,
        0x03 => CartridgeType.ROMMBC1RAMBattery,
        _ => throw new ArgumentOutOfRangeException()
    };

    private RomSizeInfo GetRomSizeInfo() => _cartridgeHeaderMemory[RomSizeFlag] switch
    {
        0x00 => new RomSizeInfo(SizeBytes: 32 * KiloByte, BankCount: 2),
        0x01 => new RomSizeInfo(SizeBytes: 64 * KiloByte, BankCount: 4),
        0x02 => new RomSizeInfo(SizeBytes: 128 * KiloByte, BankCount: 8),
        0x03 => new RomSizeInfo(SizeBytes: 256 * KiloByte, BankCount: 16),
        0x04 => new RomSizeInfo(SizeBytes: 512 * KiloByte, BankCount: 32),
        0x05 => new RomSizeInfo(SizeBytes: 1024 * KiloByte, BankCount: 64),
        0x06 => new RomSizeInfo(SizeBytes: 2048 * KiloByte, BankCount: 128),
        0x52 => new RomSizeInfo(SizeBytes: 1152 * KiloByte, BankCount: 72),
        0x53 => new RomSizeInfo(SizeBytes: 1280 * KiloByte, BankCount: 80),
        0x54 => new RomSizeInfo(SizeBytes: 1536 * KiloByte, BankCount: 96),
        _ => throw new ArgumentOutOfRangeException()
    };

    private RamSizeInfo GetRamSizeInfo() => _cartridgeHeaderMemory[RamSizeFlag] switch
    {
        0 => new RamSizeInfo(SizeBytes: 0, BankCount: 1),
        1 => new RamSizeInfo(SizeBytes: 2 * KiloByte, BankCount: 1),
        2 => new RamSizeInfo(SizeBytes: 8 * KiloByte, BankCount: 1),
        3 => new RamSizeInfo(SizeBytes: 32 * KiloByte, BankCount: 4),
        4 => new RamSizeInfo(SizeBytes: 128 * KiloByte, BankCount: 16),
        _ => throw new ArgumentOutOfRangeException()
    };

    private DestinationCode GetDestinationCode() => _cartridgeHeaderMemory[DestinationCodeFlag] switch
    {
        0 => DestinationCode.Japanese,
        1 => DestinationCode.NonJapanese,
        _ => throw new ArgumentOutOfRangeException()
    };

    private LicenseCode GetLicenseCode() => _cartridgeHeaderMemory[LicenseCodeFlag] switch
    {
        0x00 => LicenseCode.Unknown,
        0x01 => LicenseCode.Nintendo,
        0x79 => LicenseCode.Accolade,
        0xA4 => LicenseCode.Konami,
        _ => LicenseCode.Unknown
    };
}