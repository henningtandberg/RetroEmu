using RetroEmu.GB.TestSetup;

namespace RetroEmu.GB.Tests.CartridgeTests;

public abstract class TestCartridgeFactory
{
    public static byte[] CreateNoMbcCartridge() =>
        CartridgeBuilder
            .Create()
            .WithGameTitle("NO MBC")
            .WithGameBoyCartridgeType(0x00)
            .WithRomSize(0x00)
            .WithRamSize(0x00)
            .WithDestinationCode(0x01)
            .WithLicenseCodeOld(0xA4)
            .Build();
    
    public static byte[] CreateMbc1Cartridge() =>
        CartridgeBuilder
            .Create()
            .WithGameTitle("MBC1")
            .WithGameBoyCartridgeType(0x01)
            .WithRomSize(0x01)
            .WithRamSize(0x01)
            .WithDestinationCode(0x01)
            .WithLicenseCodeOld(0xA4)
            .Build();
}