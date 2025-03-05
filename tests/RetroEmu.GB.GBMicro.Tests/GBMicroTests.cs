using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;
using Xunit.Abstractions;

namespace RetroEmu.GB.GBMicro.Tests;

public class GBMicroTests(ITestOutputHelper output)
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
            processor.SetProgramCounter(0x0100))
        .BuildGameBoy();
    
    [Theory]
    [InlineData("Resources/oam_write_l0_a.gb")]
    [InlineData("Resources/poweron_bgp_000.gb")]
    [InlineData("Resources/poweron_div_000.gb")]
    [InlineData("Resources/poweron_div_004.gb")]
    [InlineData("Resources/poweron_div_005.gb")]
    [InlineData("Resources/poweron_dma_000.gb")]
    [InlineData("Resources/poweron_if_000.gb")]
    [InlineData("Resources/poweron_joy_000.gb")]
    [InlineData("Resources/poweron_lcdc_000.gb")]
    [InlineData("Resources/poweron_ly_000.gb")]
    [InlineData("Resources/poweron_ly_119.gb")]
    [InlineData("Resources/poweron_ly_120.gb")]
    [InlineData("Resources/poweron_ly_233.gb")]
    [InlineData("Resources/poweron_ly_234.gb")]
    [InlineData("Resources/poweron_lyc_000.gb")]
    [InlineData("Resources/poweron_oam_000.gb")]
    [InlineData("Resources/poweron_oam_005.gb")]
    [InlineData("Resources/poweron_oam_006.gb")]
    [InlineData("Resources/poweron_oam_069.gb")]
    [InlineData("Resources/poweron_oam_070.gb")]
    [InlineData("Resources/poweron_oam_119.gb")]
    [InlineData("Resources/poweron_oam_120.gb")]
    [InlineData("Resources/poweron_oam_121.gb")]
    [InlineData("Resources/poweron_oam_183.gb")]
    [InlineData("Resources/poweron_oam_184.gb")]
    [InlineData("Resources/poweron_oam_233.gb")]
    [InlineData("Resources/poweron_oam_234.gb")]
    [InlineData("Resources/poweron_oam_235.gb")]
    [InlineData("Resources/poweron_obp0_000.gb")]
    [InlineData("Resources/poweron_obp1_000.gb")]
    [InlineData("Resources/poweron_sb_000.gb")]
    [InlineData("Resources/poweron_sc_000.gb")]
    [InlineData("Resources/poweron_scx_000.gb")]
    [InlineData("Resources/poweron_scy_000.gb")]
    [InlineData("Resources/poweron_stat_000.gb")]
    [InlineData("Resources/poweron_stat_005.gb")]
    [InlineData("Resources/poweron_stat_006.gb")]
    [InlineData("Resources/poweron_stat_007.gb")]
    [InlineData("Resources/poweron_stat_026.gb")]
    [InlineData("Resources/poweron_stat_027.gb")]
    [InlineData("Resources/poweron_stat_069.gb")]
    [InlineData("Resources/poweron_stat_070.gb")]
    [InlineData("Resources/poweron_stat_119.gb")]
    [InlineData("Resources/poweron_stat_120.gb")]
    [InlineData("Resources/poweron_stat_121.gb")]
    [InlineData("Resources/poweron_stat_140.gb")]
    [InlineData("Resources/poweron_stat_141.gb")]
    [InlineData("Resources/poweron_stat_183.gb")]
    [InlineData("Resources/poweron_stat_184.gb")]
    [InlineData("Resources/poweron_stat_234.gb")]
    [InlineData("Resources/poweron_stat_235.gb")]
    [InlineData("Resources/poweron_tac_000.gb")]
    [InlineData("Resources/poweron_tima_000.gb")]
    [InlineData("Resources/poweron_tma_000.gb")]
    [InlineData("Resources/poweron_vram_000.gb")]
    [InlineData("Resources/poweron_vram_025.gb")]
    [InlineData("Resources/poweron_vram_026.gb")]
    [InlineData("Resources/poweron_vram_069.gb")]
    [InlineData("Resources/poweron_vram_070.gb")]
    [InlineData("Resources/poweron_vram_139.gb")]
    [InlineData("Resources/poweron_vram_140.gb")]
    [InlineData("Resources/poweron_vram_183.gb")]
    [InlineData("Resources/poweron_vram_184.gb")]
    [InlineData("Resources/poweron_wx_000.gb")]
    [InlineData("Resources/poweron_wy_000.gb")]
    public void RunMicroTest(String path)
    {
        var rom = File.ReadAllBytes(path);
        _gameBoy.Load(rom);

        var maxIterations = 200_000;
        for (var i = 0; i < maxIterations; i++)
        {
            _ = _gameBoy.Update();

            if (_gameBoy.GetMemory().Read(0xFF82) != 0)
            {
                break;
            }
        }

        if (_gameBoy.GetMemory().Read(0xFF82) != 0x01)
        {
            // Test failed, print output
            output.WriteLine("Test result: " + _gameBoy.GetMemory().Read(0xFF80));
            output.WriteLine("Expected: " + _gameBoy.GetMemory().Read(0xFF81));
            output.WriteLine("Disclaimer, result and expected might match if the test uses 'not equal'");
        }
        Assert.Equal(0x01, _gameBoy.GetMemory().Read(0xFF82));
    }
}