using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

namespace RetroEmu.GB.GBMicro.Tests;

public class GBMicroDMATests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor => processor.SetProgramCounter(0x0100))
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
    public void RunMicroTest(string path)
    {
        var cartridge_memory = File.ReadAllBytes(path);
        _gameBoy.Load(cartridge_memory);

        const int maxIterations = 200_000;
        _gameBoy.RunWhile(Func, maxIterations);

        var expected = _gameBoy.GetMemory().Read(0xFF81);
        var actual = _gameBoy.GetMemory().Read(0xFF80);
        Assert.Equal(expected, actual);
        
        return;
        
        bool Func() => 
            _gameBoy.GetMemory().Read(0xFF82) != 0x01
            && _gameBoy.GetMemory().Read(0xFF82) != 0xFF;
    }
}