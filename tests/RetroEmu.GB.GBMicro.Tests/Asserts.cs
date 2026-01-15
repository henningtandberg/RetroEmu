using RetroEmu.Devices.DMG;

namespace RetroEmu.GB.GBMicro.Tests;

/// <summary>
/// From GBMicro documentation: https://github.com/aappleby/GBMicrotest
/// 
/// Most tests will complete in a few hundred cycles, except for those that need to check behavior after VBLANK.
/// The test will write results to ram at 0xFF80-0xFF82 and then display either stripes on the LCD if the test passes,
/// or black if the test fails.
/// 
/// 0xFF80 - Test result
/// 0xFF81 - Expected result
/// 0xFF82 - 0x01 if the test passed, 0xFF if the test failed.
/// </summary>
public static class Asserts
{
    public static void AssertValueAt0xFF80IsEqualToValueAt0xFF81(this IAddressBus addressBus)
    {
        var actual = addressBus.Read(0xFF80);
        var expected = addressBus.Read(0xFF81);
        Assert.Equal(expected, actual);
    }

    public static void AssertValueAt0xFF82IsEqualTo0x01(this IAddressBus addressBus)
    {
        var result = addressBus.Read(0xFF82);
        Assert.Equal(0x01, result);
    }
}