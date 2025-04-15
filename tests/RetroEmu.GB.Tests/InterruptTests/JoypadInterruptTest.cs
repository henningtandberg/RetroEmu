using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class JoypadInterruptTest
{
    private const byte JoypadInterruptDidNotTriggerValue = 0x17;
    private const byte JoypadInterruptDidTriggerValue = 0xAA;

    private const byte DPadEnabled = 0x20;
    private const byte ButtonsEnabled = 0x10;
    
    private const byte DPadRightOrButtonA = 0x00;
    private const byte DPadLeftOrButtonB = 0x01;
    private const byte DPadUpOrButtonSelect = 0x02;
    private const byte DPadDownOrButtonStart = 0x03;
    
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
        {
            processor.SetInterruptMasterEnableToValue(true);
            processor.SetJoypadInterruptEnableToValue(true);
        })
        .BuildGameBoy();
    
    [Theory]
    [InlineData(DPadEnabled, DPadRightOrButtonA)]
    [InlineData(DPadEnabled, DPadLeftOrButtonB)]
    [InlineData(DPadEnabled, DPadUpOrButtonSelect)]
    [InlineData(DPadEnabled, DPadDownOrButtonStart)]
    [InlineData(ButtonsEnabled, DPadRightOrButtonA)]
    [InlineData(ButtonsEnabled, DPadLeftOrButtonB)]
    [InlineData(ButtonsEnabled, DPadUpOrButtonSelect)]
    [InlineData(ButtonsEnabled, DPadDownOrButtonStart)]
    public void JoypadInput_DPadOrButtonsEnabled_JoypadInterruptHandlerTriggered(byte enabler, byte joypadInput)
    {
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                Opcode.Ld_A_N8,
                enabler,
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
        
        _gameBoy.Load(cartridge);
        
        _gameBoy.RunFor(5); // Run until P1 is enabled with dpad or button input
        switch (enabler)
        {
            case ButtonsEnabled:
                _gameBoy.ButtonPressed((Button)joypadInput);
                break;
            case DPadEnabled:
                _gameBoy.DPadPressed((DPad)joypadInput);
                break;
        }
        _gameBoy.RunFor(2);

        var actual = ((ITestableProcessor)_gameBoy.GetProcessor()).GetValueOfRegisterA();
        Assert.Equal(JoypadInterruptDidTriggerValue, actual);
    }
}