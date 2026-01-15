using RetroEmu.Devices.DMG;
using RetroEmu.GB.TestSetup;

using static RetroEmu.GB.GBMicro.Tests.Asserts;

namespace RetroEmu.GB.GBMicro.Tests;

public class TimerTests
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder.CreateBuilder().BuildGameBoy();
    
    [Fact(Skip = "May need better timing")]
    public void TimaBootPhaseTest()
    {
        var path = "Resources/timer/004-tima_boot_phase.gb";
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
        
    [Fact(Skip = "May need better timing")]
    public void TimaCycleTimerTest()
    {
        var path = "Resources/timer/004-tima_cycle_timer.gb";
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
        
    [Theory]
    [InlineData("Resources/timer/timer_tima_inc_256k_a.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_b.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_c.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_d.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_e.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_f.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_g.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_h.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_i.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_j.gb")]
    [InlineData("Resources/timer/timer_tima_inc_256k_k.gb")]
    [InlineData("Resources/timer/timer_tima_inc_64k_a.gb")]
    [InlineData("Resources/timer/timer_tima_inc_64k_b.gb")]
    [InlineData("Resources/timer/timer_tima_inc_64k_c.gb")]
    [InlineData("Resources/timer/timer_tima_inc_64k_d.gb")]
    public void TimerTimaIncTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
    
    [Theory]
    [InlineData("Resources/timer/timer_tima_phase_a.gb")]
    [InlineData("Resources/timer/timer_tima_phase_b.gb")]
    [InlineData("Resources/timer/timer_tima_phase_c.gb")]
    [InlineData("Resources/timer/timer_tima_phase_d.gb")]
    [InlineData("Resources/timer/timer_tima_phase_e.gb")]
    [InlineData("Resources/timer/timer_tima_phase_f.gb")]
    [InlineData("Resources/timer/timer_tima_phase_g.gb")]
    [InlineData("Resources/timer/timer_tima_phase_h.gb")]
    [InlineData("Resources/timer/timer_tima_phase_i.gb")]
    [InlineData("Resources/timer/timer_tima_phase_j.gb")]
    public void TimerTimaPhaseTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
    
    [Theory]
    [InlineData("Resources/timer/timer_div_phase_c.gb")]
    [InlineData("Resources/timer/timer_div_phase_d.gb")]
    public void RunTimerDivReloadTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
    
    [Theory]
    [InlineData("Resources/timer/timer_tima_reload_256k_a.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_b.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_c.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_d.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_e.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_f.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_g.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_h.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_i.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_j.gb")]
    [InlineData("Resources/timer/timer_tima_reload_256k_k.gb")]
    public void TimerTimaReloadTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
        
    [Theory(Skip = "Might need better instruction timing for these tests (Such as stores happening at the end of the instruction)")]
    [InlineData("Resources/timer/timer_tima_write_a.gb")]
    [InlineData("Resources/timer/timer_tima_write_b.gb")]
    [InlineData("Resources/timer/timer_tima_write_c.gb")]
    [InlineData("Resources/timer/timer_tima_write_d.gb")]
    [InlineData("Resources/timer/timer_tima_write_e.gb")]
    [InlineData("Resources/timer/timer_tima_write_f.gb")]
    [InlineData("Resources/timer/timer_tma_write_a.gb")]
    [InlineData("Resources/timer/timer_tma_write_b.gb")]
    public void TimerTimaWriteTests(string path)
    {
        var cartridgeMemory = File.ReadAllBytes(path);
        var addressBus = _gameBoy.GetMemory();
        _gameBoy.Load(cartridgeMemory);

        _gameBoy.RunWhile(() => addressBus.ValueAt0xFF82IsZero(), RunningConditions.MaxInstructions);
        
        AssertGBMicroCondition(addressBus, path);
    }
}