using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

using static RetroEmu.GB.GBMicro.Tests.Asserts;

namespace RetroEmu.GB.GBMicro.Tests;

public class HaltTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Theory(Skip = "Needs better implementation of HALT")]
    [InlineData("Resources/halt/halt_bug.gb")]
    [InlineData("Resources/halt/halt_op_dupe.gb")]
    [InlineData("Resources/halt/halt_op_dupe_delay.gb")]
    public void RunHaltTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
}