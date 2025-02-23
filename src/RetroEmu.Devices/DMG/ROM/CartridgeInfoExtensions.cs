using System;
using System.Linq;
using System.Text;

namespace RetroEmu.Devices.DMG.ROM
{
    public static class CartridgeExtensions
    {
        public static string GetTitle(this byte[] rom)
        {
            return Encoding.ASCII.GetString(rom[0x0134..0x0142]);
        }

        public static bool HasColor(this byte[] rom)
        {
            return rom[0x0143] == 0x80;
        }

        public static bool HasSuperGameBoyFunctions(this byte[] rom)
        {
            return rom[0x0146] == 0x03;
        }

        public static CartridgeType GetCartridgeType(this byte[] rom)
        {
            var cartridgeTypeList = Enum
                .GetValues(typeof(CartridgeType))
                .Cast<CartridgeType>()
                .ToList();
            byte romCode = rom[0x0147];
            
            return cartridgeTypeList[romCode];
        }

        public static RomSizeInfo GetRomSizeInfo(this byte[] rom)
        {
            uint bankCount = rom[0x0148] switch
            {
                0 => 2,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new RomSizeInfo
            {
                RomSizeBytes = (1 << 14) * bankCount,
                RomBankCount = bankCount
            };
        }
        
        public static RamSizeInfo GetRamSizeInfo(this byte[] rom)
        {
            (uint bankCount, uint ramSize) = rom[0x0149] switch
            {
                0 => ((uint)1, (uint)1 << 11),
                _ => throw new ArgumentOutOfRangeException()
            };

            return new RamSizeInfo
            {
                RamSizeBytes = ramSize,
                RamBankCount = bankCount
            };
        }

        public static DestinationCode GetDestinationCode(this byte[] rom)
        {
            return rom[0x014A] switch
            {
                0 => DestinationCode.Japanese,
                1 => DestinationCode.NonJapanese,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static LicenseCode GetLicenseCode(this byte[] rom)
        {
            return rom[0x014B] switch
            {
                0x00 => LicenseCode.Unknown,
                0x01 => LicenseCode.Nintendo,
                //0x33 => handle new style license code
                0x79 => LicenseCode.Accolade,
                0xA4 => LicenseCode.Konami,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}