using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

namespace RetroEmu.GB.GBMicro.Tests;

public class DirectMemoryAccessTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .BuildGameBoy();
    
    [Theory]
    [InlineData("Resources/dma/400-dma.gb")]
    [InlineData("Resources/dma/dma_0x1000.gb")]
    [InlineData("Resources/dma/dma_0x9000.gb")]
    [InlineData("Resources/dma/dma_0xA000.gb")]
    [InlineData("Resources/dma/dma_0xC000.gb")]
    [InlineData("Resources/dma/dma_0xE000.gb")]
    [InlineData("Resources/dma/dma_basic.gb")]
    [InlineData("Resources/dma/dma_timing_a.gb")]
    public void DirectMemoryAccess_ValueAt0xFF80IsEqualToValueAt0xFF81AndValueAt0xFF82IsEqualTo0x01(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        addressBus.AssertValueAt0xFF80IsEqualToValueAt0xFF81();
        addressBus.AssertValueAt0xFF82IsEqualTo0x01();
    }
}