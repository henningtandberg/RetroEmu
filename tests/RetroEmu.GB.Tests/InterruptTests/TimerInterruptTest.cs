using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class TimerInterruptTest
{
    private const byte TimerInterruptDidNotTriggerValue = 0x17;
    private const byte TimerInterruptDidTriggerValue = 0xAA;
    
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
        {
            processor.SetInterruptMasterEnableToValue(true);
            processor.SetTimerInterruptEnableToValue(true);
            // This will set the timer to overflow after four instructions
            processor.SetTimerCounter(0xFF);
            processor.SetTimerModulo(0x00);
            processor.SetTimerControl(0b101);
        })
        .BuildGameBoy();
    
    private readonly byte[] _timerInterruptProgramCartridge = CartridgeBuilder
        .Create()
        .WithProgram([
            Opcode.Nop,
            Opcode.Nop,
            Opcode.Nop,
            Opcode.Nop,
            Opcode.Ld_A_N8,
            TimerInterruptDidNotTriggerValue
        ])
        .WithTimerInterruptHandler([
            Opcode.Ld_A_N8,
            TimerInterruptDidTriggerValue,
        ])
        .Build();

    [Fact]
    public void InterruptProgram_TimerOverflows_TimerInterruptTriggered()
    {
        _gameBoy.Load(_timerInterruptProgramCartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        processor.SetProgramCounter(0x0150); // Skip program start routine at 0x0100 (NOP + JP N16)
        // This will set the timer to overflow after four instructions
        processor.SetTimerCounter(0xFF);
        processor.SetTimerModulo(0x00);
        processor.SetTimerControl(0b101);
        
        _gameBoy.RunFor(amountOfInstructions: 5);
        
        Assert.Equal(TimerInterruptDidTriggerValue, processor.GetValueOfRegisterA());
    }
}