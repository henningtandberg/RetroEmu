using RetroEmu.Devices;
using RetroEmu.GB.TestSetup;

using static RetroEmu.GB.GBMicro.Tests.Asserts;

namespace RetroEmu.GB.GBMicro.Tests;

public class DirectMemoryAccessTests
{
    private readonly ITestableEmulator _gameBoy = TestGameBoyBuilder.CreateBuilder().Build();
    
    [Theory]
    [InlineData("Resources/dma/400-dma.gb")]
    [InlineData("Resources/dma/dma_0x1000.gb")]
    [InlineData("Resources/dma/dma_0x9000.gb")]
    [InlineData("Resources/dma/dma_0xA000.gb")]
    [InlineData("Resources/dma/dma_0xC000.gb")]
    [InlineData("Resources/dma/dma_0xE000.gb")]
    [InlineData("Resources/dma/dma_basic.gb")]
    [InlineData("Resources/dma/dma_timing_a.gb")]
    public void RunDirectMemoryAccess(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
}