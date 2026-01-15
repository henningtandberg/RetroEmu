using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

using static RetroEmu.GB.GBMicro.Tests.Asserts;

namespace RetroEmu.GB.GBMicro.Tests;

public class PowerOnTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Theory]
    [InlineData("Resources/poweron/poweron.gb")]
    [InlineData("Resources/poweron/poweron_bgp_000.gb")]
    [InlineData("Resources/poweron/poweron_div_000.gb")]
    [InlineData("Resources/poweron/poweron_div_004.gb")]
    [InlineData("Resources/poweron/poweron_div_005.gb")]
    [InlineData("Resources/poweron/poweron_dma_000.gb")]
    [InlineData("Resources/poweron/poweron_if_000.gb")]
    [InlineData("Resources/poweron/poweron_joy_000.gb")]
    [InlineData("Resources/poweron/poweron_lcdc_000.gb")]
    [InlineData("Resources/poweron/poweron_ly_000.gb")]
    [InlineData("Resources/poweron/poweron_ly_119.gb")]
    [InlineData("Resources/poweron/poweron_ly_120.gb")]
    [InlineData("Resources/poweron/poweron_ly_233.gb")]
    [InlineData("Resources/poweron/poweron_ly_234.gb")]
    [InlineData("Resources/poweron/poweron_lyc_000.gb")]
    [InlineData("Resources/poweron/poweron_oam_000.gb")]
    [InlineData("Resources/poweron/poweron_oam_005.gb")]
    [InlineData("Resources/poweron/poweron_oam_006.gb")]
    [InlineData("Resources/poweron/poweron_oam_069.gb")]
    [InlineData("Resources/poweron/poweron_oam_070.gb")]
    [InlineData("Resources/poweron/poweron_oam_119.gb")]
    [InlineData("Resources/poweron/poweron_oam_120.gb")]
    [InlineData("Resources/poweron/poweron_oam_121.gb")]
    [InlineData("Resources/poweron/poweron_oam_183.gb")]
    [InlineData("Resources/poweron/poweron_oam_184.gb")]
    [InlineData("Resources/poweron/poweron_oam_233.gb")]
    [InlineData("Resources/poweron/poweron_oam_234.gb")]
    [InlineData("Resources/poweron/poweron_oam_235.gb")]
    [InlineData("Resources/poweron/poweron_obp0_000.gb")]
    [InlineData("Resources/poweron/poweron_obp1_000.gb")]
    [InlineData("Resources/poweron/poweron_sb_000.gb")]
    [InlineData("Resources/poweron/poweron_sc_000.gb")]
    [InlineData("Resources/poweron/poweron_scx_000.gb")]
    [InlineData("Resources/poweron/poweron_scy_000.gb")]
    [InlineData("Resources/poweron/poweron_stat_000.gb")]
    [InlineData("Resources/poweron/poweron_stat_005.gb")]
    [InlineData("Resources/poweron/poweron_stat_006.gb")]
    [InlineData("Resources/poweron/poweron_stat_007.gb")]
    [InlineData("Resources/poweron/poweron_stat_026.gb")]
    [InlineData("Resources/poweron/poweron_stat_027.gb")]
    [InlineData("Resources/poweron/poweron_stat_069.gb")]
    [InlineData("Resources/poweron/poweron_stat_070.gb")]
    [InlineData("Resources/poweron/poweron_stat_119.gb")]
    [InlineData("Resources/poweron/poweron_stat_120.gb")]
    [InlineData("Resources/poweron/poweron_stat_121.gb")]
    [InlineData("Resources/poweron/poweron_stat_140.gb")]
    [InlineData("Resources/poweron/poweron_stat_141.gb")]
    [InlineData("Resources/poweron/poweron_stat_183.gb")]
    [InlineData("Resources/poweron/poweron_stat_184.gb")]
    [InlineData("Resources/poweron/poweron_stat_234.gb")]
    [InlineData("Resources/poweron/poweron_stat_235.gb")]
    [InlineData("Resources/poweron/poweron_tac_000.gb")]
    [InlineData("Resources/poweron/poweron_tima_000.gb")]
    [InlineData("Resources/poweron/poweron_tma_000.gb")]
    [InlineData("Resources/poweron/poweron_vram_000.gb")]
    [InlineData("Resources/poweron/poweron_vram_025.gb")]
    [InlineData("Resources/poweron/poweron_vram_026.gb")]
    [InlineData("Resources/poweron/poweron_vram_069.gb")]
    [InlineData("Resources/poweron/poweron_vram_070.gb")]
    [InlineData("Resources/poweron/poweron_vram_139.gb")]
    [InlineData("Resources/poweron/poweron_vram_140.gb")]
    [InlineData("Resources/poweron/poweron_vram_183.gb")]
    [InlineData("Resources/poweron/poweron_vram_184.gb")]
    [InlineData("Resources/poweron/poweron_wx_000.gb")]
    [InlineData("Resources/poweron/poweron_wy_000.gb")]
    public void RunPowerOnTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
}