using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.InterruptTests;

public class BasicInterruptTest
{
    [Theory]
    [InlineData(false, false, false, false)]
    [InlineData(false, false, true, false)]
    [InlineData(false, true, false, false)]
    [InlineData(false, true, true, false)]
    [InlineData(true, false, false, false)]
    [InlineData(true, false, true, false)]
    [InlineData(true, true, false, false)]
    [InlineData(true, true, true, true)]
    public static void
        InterruptProgram_TriggerInterrupt_CheckIfInterruptTriggered(bool IME, bool IE, bool triggerInterrupt, bool expectInterruptTriggered)
    {
        const byte interruptDidTriggerValue = 0x01;
        const byte interruptDidNotTriggerValue = 0x02;
        const ushort serialInterruptAddress = 0x58;

        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0001))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_A_N8,
                [0x0002] = interruptDidNotTriggerValue,
                [serialInterruptAddress] = Opcode.Ld_A_N8,
                [serialInterruptAddress + 1] = interruptDidTriggerValue,
            })
            .BuildGameBoy();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        processor.SetInterruptMasterEnableToValue(IME);
        processor.SetSerialInterruptEnableToValue(IE);

        if(triggerInterrupt)
        {
            processor.GenerateSerialInterrupt();
        }

        gameBoy.Update();
        
        var result = processor.GetValueOfRegisterA();
        Assert.Contains(result, [interruptDidTriggerValue, interruptDidNotTriggerValue]);
        var didInterruptTrigger = result == interruptDidTriggerValue;
        Assert.Equal(didInterruptTrigger, expectInterruptTriggered);
    }
}