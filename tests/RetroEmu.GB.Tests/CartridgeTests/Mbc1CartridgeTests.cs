using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.ROM;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.CartridgeTests;

public class Mbc1CartridgeTests
{
    private readonly IGameBoy _gameBoy =
        TestGameBoyBuilder
            .CreateBuilder()
            .BuildGameBoy();
    
    private readonly byte[] _cartridgeMemory = TestCartridgeFactory.CreateMbc1Cartridge();
    
    [Fact]
    public void MBC1CartridgeIsLoaded_CartridgeHeaderIsSetCorrectly()
    {
        _gameBoy.Load(_cartridgeMemory);

        var cartridgeInfo = _gameBoy.GetCartridgeInfo();
        Assert.Equal("MBC1", cartridgeInfo.GameTitle);
        Assert.False(cartridgeInfo.HasColor);
        Assert.False(cartridgeInfo.HasGameBoySuperFunctions);
        Assert.Equal(CartridgeType.ROMMBC1, cartridgeInfo.CartridgeType);
        Assert.Equal((uint)64 * 1024, cartridgeInfo.RomSizeInfo.SizeBytes);
        Assert.Equal((uint)4, cartridgeInfo.RomSizeInfo.BankCount);
        Assert.Equal((uint)2 * 1024, cartridgeInfo.RamSizeInfo.SizeBytes);
        Assert.Equal((uint)1, cartridgeInfo.RamSizeInfo.BankCount);
        Assert.Equal(DestinationCode.NonJapanese, cartridgeInfo.DestinationCode);
        Assert.Equal(LicenseCode.Konami, cartridgeInfo.LicenseCode);
    }
}