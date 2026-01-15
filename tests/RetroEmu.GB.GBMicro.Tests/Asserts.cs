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
    public static void AssertGBMicroCondition(IAddressBus addressBus, string testRomName)
    {
        AssertValueAt0xFF80IsEqualToValueAt0xFF81(addressBus, testRomName);
        AssertValueAt0xFF82IsEqualTo0x01(addressBus, testRomName);
    }

    private static void AssertValueAt0xFF80IsEqualToValueAt0xFF81(IAddressBus addressBus, string testRomName)
    {
        var actual = addressBus.Read(0xFF80);
        var expected = addressBus.Read(0xFF81);
        Assert.True(actual == expected,
            $"{testRomName} failed due to Actual(0xFF80): {actual} != Expected(0xFF81): {expected}");
    }

    private static void AssertValueAt0xFF82IsEqualTo0x01(IAddressBus addressBus, string testRomName)
    {
        const byte expected = 0x01;
        var actual = addressBus.Read(0xFF82);
        
        Assert.True(expected == actual,
            $"{testRomName} failed due to Expected(0xFF81): {expected} != Actual(0xFF80): {actual}");
    }
}