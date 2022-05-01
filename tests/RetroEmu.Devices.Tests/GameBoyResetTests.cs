using Xunit;
using RetroEmu.Devices.DMG;
using Microsoft.Extensions.DependencyInjection;

namespace RetroEmu.Devices.Tests
{
    public class GameBoyResetTests
    {
        [Fact]
        public void WithGameBoy_ValidDummyROMIsLoaded_CartridgeIsSetCorrectly()
        {
            byte[] rom = new TestRomBuilder(355)
                .SetScrollingNintendoGraphic(CreateValidScrollingNintendoGraphic())
                .SetGameTitle("VALID TEST ROM")
                .SetGameBoyColorFlag(0x00)
                .SetGameBoySuperFlag(0x00)
                .SetGameBoyCartridgeType(0x00)
                .SetRomSize(0x00)
                .SetRamSize(0x00)
                .SetDestinationCode(0x01)
                .SetLicenseCodeOld(0xA4)
                .Build();
            IGameBoy gameBoy = CreateGameBoy();

            gameBoy.Load(rom);

            CartridgeInfo cartridgeInfo = gameBoy.GetCartridgeInfo();
            Assert.Equal("VALID TEST ROM", cartridgeInfo.GameTitle);
            Assert.False(cartridgeInfo.HasColor);
            Assert.False(cartridgeInfo.HasGameBoySuperFunctions);
            Assert.Equal(CartridgeType.RomOnly, cartridgeInfo.CartridgeType);
            Assert.Equal((uint)32768, cartridgeInfo.RomSizeInfo.RomSizeBytes);
            Assert.Equal((uint)2, cartridgeInfo.RomSizeInfo.RomBankCount);
            Assert.Equal((uint)2048, cartridgeInfo.RamSizeInfo.RamSizeBytes);
            Assert.Equal((uint)1, cartridgeInfo.RamSizeInfo.RamBankCount);
            Assert.Equal(DestinationCode.NonJapanese, cartridgeInfo.DestinationCode);
            Assert.Equal(LicenseCode.Konami, cartridgeInfo.LicenseCode);
        }

        private static byte[] CreateValidScrollingNintendoGraphic()
        {
            return new byte[]
            {
                0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B, 0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
                0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E, 0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
                0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC, 0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E
            };
        }
        
        private static IGameBoy CreateGameBoy()
        {
            return new ServiceCollection()
                .AddDMG()
                .BuildServiceProvider()
                .GetRequiredService<IGameBoy>();
        }
    }
}
