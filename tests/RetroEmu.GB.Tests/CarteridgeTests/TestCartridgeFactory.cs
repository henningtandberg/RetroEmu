using RetroEmu.GB.TestSetup;

namespace RetroEmu.GB.Tests.CarteridgeTests;

public class TestCartridgeFactory
{
    public static byte[] CreateNoMbcCartridge() =>
        CartridgeBuilder
            .Create()
            .WithGameTitle("VALID TEST ROM")
            .WithGameBoyCartridgeType(0x00)
            .WithRomSize(0x00)
            .WithRamSize(0x00)
            .WithDestinationCode(0x01)
            .WithLicenseCodeOld(0xA4)
            .Build();
}