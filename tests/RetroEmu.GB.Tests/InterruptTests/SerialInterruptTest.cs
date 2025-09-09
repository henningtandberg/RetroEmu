using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.InterruptTests;

public class SerialInterruptTest
{
    private const byte InterruptDidTriggerValue = 0x01;
    private const byte InterruptDidNotTriggerValue = 0x02;

    [Theory (Skip = "Need to implement serial interrupts properly")]
    [InlineData(false, false, false, false)]
    [InlineData(false, false, true, false)]
    [InlineData(false, true, false, false)]
    [InlineData(false, true, true, false)]
    [InlineData(true, false, false, false)]
    [InlineData(true, false, true, false)]
    [InlineData(true, true, false, false)]
    [InlineData(true, true, true, true)]
    public static void InterruptProgram_SerialInterrupt_CheckIfInterruptTriggered(
        bool IME, bool IE, bool triggerInterrupt, bool expectInterruptTriggered)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor =>
            {
                processor.SetInterruptMasterEnableToValue(IME);
                processor.SetSerialInterruptEnableToValue(IE);
            })
            .BuildGameBoy();
        var cartridge = CartridgeBuilder
            .Create()
            .WithProgram([
                Opcode.Ld_A_N8,
                InterruptDidNotTriggerValue,
            ])
            .WithSerialInterruptHandler([
                Opcode.Ld_A_N8,
                InterruptDidTriggerValue
            ])
            .Build();
        gameBoy.Load(cartridge);
        var processor = (ITestableProcessor)gameBoy.GetProcessor();

        gameBoy.Update();
        if (triggerInterrupt)
        {
            processor.GenerateSerialInterrupt();
        }
        gameBoy.Update();
        
        var result = processor.GetValueOfRegisterA();
        Assert.Contains(result, [InterruptDidTriggerValue, InterruptDidNotTriggerValue]);
        var didInterruptTrigger = result == InterruptDidTriggerValue;
        Assert.Equal(didInterruptTrigger, expectInterruptTriggered);
    }
}