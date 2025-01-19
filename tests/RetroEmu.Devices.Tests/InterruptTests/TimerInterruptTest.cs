using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.InterruptTests;

public class TimerInterruptTest
{
    [Fact]
    public static void InterruptProgram_TimerOverflows_TimerInterruptTriggered()
    {
        const byte interruptDidTriggerValue = 0x01;
        const byte interruptDidNotTriggerValue = 0x02;
        const ushort timerInterruptAddress = 0x50;

        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0001))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Nop,
                [0x0002] = Opcode.Nop,
                [0x0003] = Opcode.Nop,
                [0x0004] = Opcode.Nop,
                [0x0005] = interruptDidNotTriggerValue,
                [timerInterruptAddress] = Opcode.Ld_A_N8,
                [timerInterruptAddress + 1] = interruptDidTriggerValue,
                [0xFF05] = 0xFF,
                [0xFF06] = 0x00,
                [0xFF07] = 0b101,
            })
            .BuildGameBoy();

        var processor = gameBoy.GetProcessor();
        processor.SetTimerSpeed(4);
        processor.SetInterruptMasterEnable(true);
        processor.SetInterruptEnable(InterruptType.Timer, true);

        for (var i = 0; i < 5; ++i)
        {
            gameBoy.Update();
        }
        
        var result = processor.GetValueOfRegisterA();
        Assert.Contains(result, [interruptDidTriggerValue, interruptDidNotTriggerValue]);

        var didInterruptTrigger = result == interruptDidTriggerValue;
        Assert.True(didInterruptTrigger);
    }
}