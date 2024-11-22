using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.InterruptTests;

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
        byte interruptDidTriggerValue = 0x01;
        byte interruptDidNotTriggerValue = 0x02;
        ushort serialInterruptAddress = 0x58;

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
               [(ushort)(serialInterruptAddress + 1)] = interruptDidTriggerValue,
           })
           .BuildGameBoy();

        var processor = gameBoy.GetProcessor();
        processor.SetInterruptMasterEnable(IME);
        processor.SetInterruptEnable(InterruptType.Serial, IE);

        if(triggerInterrupt)
        {
            processor.GenerateInterrupt(InterruptType.Serial);
        }

        gameBoy.Update();
        
        var result = processor.GetValueOfRegisterA();
        Assert.Contains(result, [interruptDidTriggerValue, interruptDidNotTriggerValue]);

        bool didInterruptTrigger = false;
        if (result == interruptDidTriggerValue)
        {
            didInterruptTrigger = true;
        }
        Assert.Equal(didInterruptTrigger, expectInterruptTriggered);
    }
}