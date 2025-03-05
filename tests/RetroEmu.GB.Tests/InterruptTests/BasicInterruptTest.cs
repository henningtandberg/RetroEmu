using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class BasicInterruptTest
{
    private const byte InterruptDidTriggerValue = 0x01;
    private const byte InterruptDidNotTriggerValue = 0x02;
    private const ushort SerialInterruptAddress = 0x58;

    [Theory]
    [InlineData(false, false, false, false)]
    [InlineData(false, false, true, false)]
    [InlineData(false, true, false, false)]
    [InlineData(false, true, true, false)]
    [InlineData(true, false, false, false)]
    [InlineData(true, false, true, false)]
    [InlineData(true, true, false, false)]
    [InlineData(true, true, true, true)]
    public static void InterruptProgram_TriggerInterrupt_CheckIfInterruptTriggered(
        bool IME, bool IE, bool triggerInterrupt, bool expectInterruptTriggered)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor =>
            {
                processor.Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
                processor.SetProgramCounter(0x0001);
                processor.SetInterruptMasterEnableToValue(IME);
                processor.SetSerialInterruptEnableToValue(IE);

                if(triggerInterrupt)
                {
                    processor.GenerateSerialInterrupt();
                }
            })
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Ld_A_N8,
                [0x0002] = InterruptDidNotTriggerValue,
                [SerialInterruptAddress] = Opcode.Ld_A_N8,
                [SerialInterruptAddress + 1] = InterruptDidTriggerValue
            })
            .BuildGameBoy();

        gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        var result = processor.GetValueOfRegisterA();
        Assert.Contains(result, [InterruptDidTriggerValue, InterruptDidNotTriggerValue]);
        var didInterruptTrigger = result == InterruptDidTriggerValue;
        Assert.Equal(didInterruptTrigger, expectInterruptTriggered);
    }
}