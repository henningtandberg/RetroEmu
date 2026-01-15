using System.Text;
using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

namespace RetroEmu.GB.Blargg.Tests;

public class HaltBugTests
{
    private static readonly WireFake WireFake = new();
    
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
            processor.SetProgramCounter(0x0100))
        .WithWireFake(WireFake)
        .BuildGameBoy();
    
    [Fact]
    public void Halt_Bug()
    {
        var cartridgeMemory = File.ReadAllBytes("Resources/halt_bug.gb");
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunFor(amountOfInstructions: 10_000_000);

        var actualOutput = WireFake.AllOutgoingData();
        Assert.Equal("Passed\n", Encoding.ASCII.GetString(actualOutput));
    }
}