using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class TimerInterruptTest
{
    private const byte InterruptDidTriggerValue = 0x01;
    private const byte InterruptDidNotTriggerValue = 0x02;
    private const ushort TimerInterruptAddress = 0x50;

    [Fact]
    public static void InterruptProgram_TimerOverflows_TimerInterruptTriggered()
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor =>
            {
                processor.Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
                processor.SetProgramCounter(0x0001);
                processor.SetTimerSpeed(4);
                processor.SetInterruptMasterEnableToValue(true);
                processor.SetTimerInterruptEnableToValue(true);
            })
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Nop,
                [0x0002] = Opcode.Nop,
                [0x0003] = Opcode.Nop,
                [0x0004] = Opcode.Nop,
                [0x0005] = InterruptDidNotTriggerValue,
                [TimerInterruptAddress] = Opcode.Ld_A_N8,
                [TimerInterruptAddress + 1] = InterruptDidTriggerValue,
                [0xFF05] = 0xFF,
                [0xFF06] = 0x00,
                [0xFF07] = 0b101,
            })
            .BuildGameBoy();

        gameBoy.RunFor(cycles: 5);
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        var result = processor.GetValueOfRegisterA();
        Assert.Contains(result, [InterruptDidTriggerValue, InterruptDidNotTriggerValue]);
        Assert.Equal(result, InterruptDidTriggerValue);
    }
}