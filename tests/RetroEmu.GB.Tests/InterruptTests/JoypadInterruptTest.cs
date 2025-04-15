using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class JoypadInterruptTest
{
    private const ushort CodeExecutionStartAddress = 0x0150;
    private const byte JoypadInterruptDidNotTriggerValue = 0x17;
    private const byte JoypadInterruptDidTriggerValue = 0xAA;
    
    [Fact]
    public static void InterruptProgram_ButtonAIsPressed_JoypadInterruptTriggered()
    {
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                Opcode.Ld_A_N8,
                0x10,
                Opcode.Ld_HL_N16,
                0x00,
                0xFF,
                Opcode.Ld_XHL_A,
                Opcode.Nop,
                Opcode.Ld_A_N8,
                JoypadInterruptDidNotTriggerValue
            ])
            .WithJoypadInterrruptHandler([
                Opcode.Ld_A_N8,
                JoypadInterruptDidTriggerValue
            ])
            .Build();
        
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor =>
            {
                processor.SetInterruptMasterEnableToValue(true);
                processor.SetJoypadInterruptEnableToValue(true);
            })
            .BuildGameBoy();
        gameBoy.Load(cartridge);
        
        gameBoy.RunFor(5);
        gameBoy.ButtonPressed(Button.A);
        gameBoy.RunFor(2);
        gameBoy.ButtonReleased(Button.A);

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        var result = processor.GetValueOfRegisterA();
        Assert.Equal(JoypadInterruptDidTriggerValue, result);
    }

}