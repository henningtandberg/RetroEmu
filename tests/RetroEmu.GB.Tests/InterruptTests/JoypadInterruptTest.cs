using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class JoypadInterruptTest
{
    [Fact]
    public static void InterruptProgram_ButtonAIsPressed_JoypadInterruptTriggered()
    {
        const byte joypadInterruptDidNotTriggerValue = 0x17;
        const byte joypadInterruptDidTriggerValue = 0xAA;
        const ushort joypadInterruptAddress = 0x60;
        
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor =>
            {
                processor.Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
                processor.SetProgramCounter(0x0001);
                processor.SetInterruptMasterEnableToValue(true);
                processor.SetJoypadInterruptEnableToValue(true);
            })
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_A_N8,
                [0x0002] = 0xEE, // Button A is pressed
                [0x0003] = Opcode.Ld_HL_N16,
                [0x0004] = 0x00,
                [0x0005] = 0xFF,
                [0x0006] = Opcode.Ld_XHL_A,
                [0x0007] = Opcode.Ld_A_N8,
                [0x0008] = joypadInterruptDidNotTriggerValue,
                // 0x0009 - 0x005F
                [joypadInterruptAddress] = Opcode.Ld_A_N8,
                [joypadInterruptAddress + 1] = joypadInterruptDidTriggerValue
            })
            .BuildGameBoy();

        gameBoy.RunFor(cycles: 4);

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        var result = processor.GetValueOfRegisterA();
        Assert.Equal(joypadInterruptDidTriggerValue, result);
    }
}