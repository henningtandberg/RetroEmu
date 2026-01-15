using RetroEmu.Devices.DMG;

namespace RetroEmu.GB.GBMicro.Tests;

public static class RunningConditions
{
    /// <summary>
    /// The tests should finish way before max instructions is hit,
    /// however this works as a failsafe.
    /// </summary>
    /// <returns>Maximum instructions to perform before stopping test</returns>
    public static int MaxInstructions => 200_000;

    /// <summary>
    /// Either 0x01 or 0xFF is written to 0xFF82 to indicate that the test has either passed or failed.
    /// </summary>
    /// <param name="addressBus"></param>
    /// <returns>true if value at 0xFF82 == 0</returns>
    public static bool ValueAt0xFF82IsZero(this IAddressBus addressBus) => addressBus.Read(0xFF82) == 0;
}