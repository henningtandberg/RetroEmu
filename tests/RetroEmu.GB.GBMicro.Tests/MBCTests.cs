using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

using static RetroEmu.GB.GBMicro.Tests.Asserts;

namespace RetroEmu.GB.GBMicro.Tests;

public class MBCTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Theory]
    [InlineData("Resources/mbc/mbc1_ram_banks.gb")]
    [InlineData("Resources/mbc/mbc1_rom_banks.gb")]
    public void RunMBCTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
}