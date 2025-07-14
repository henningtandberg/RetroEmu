using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.ROM;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.CartridgeTests;

public class NoMbcCartridgeTests
{
    private readonly IGameBoy _gameBoy =
        TestGameBoyBuilder
            .CreateBuilder()
            .BuildGameBoy();
    
    private readonly byte[] _cartridgeMemory = TestCartridgeFactory.CreateNoMbcCartridge();
    
    [Fact]
    public void NoMBCCartridgeIsLoaded_CartridgeHeaderIsSetCorrectly()
    {
        _gameBoy.Load(_cartridgeMemory);

        var cartridgeInfo = _gameBoy.GetCartridgeInfo();
        Assert.Equal("NO MBC", cartridgeInfo.GameTitle);
        Assert.False(cartridgeInfo.HasColor);
        Assert.False(cartridgeInfo.HasGameBoySuperFunctions);
        Assert.Equal(CartridgeType.ROMOnly, cartridgeInfo.CartridgeType);
        Assert.Equal((uint)32 * 1024, cartridgeInfo.RomSizeInfo.SizeBytes);
        Assert.Equal((uint)2, cartridgeInfo.RomSizeInfo.BankCount);
        Assert.Equal((uint)0, cartridgeInfo.RamSizeInfo.SizeBytes);
        Assert.Equal((uint)1, cartridgeInfo.RamSizeInfo.BankCount);
        Assert.Equal(DestinationCode.NonJapanese, cartridgeInfo.DestinationCode);
        Assert.Equal(LicenseCode.Konami, cartridgeInfo.LicenseCode);
    }
}