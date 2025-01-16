using System;
using System.IO;
using RetroEmu.Devices.Tests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.Devices.Tests.Blargg;

public class CpuInstructionsIndividual06LDrr(ITestOutputHelper output)
{
    [Fact]
    public void
        LDrrProgram_RunUntilFinished_AllTestsShouldPass()
    {
        var gameBoy = TestGameBoyBuilder
           .CreateBuilder()
           .WithProcessor(processor => processor.SetProgramCounter(0x0100))
           .BuildGameBoy();
        
        var rom = File.ReadAllBytes("Blargg/Resources/06-ld r,r.gb");
        gameBoy.Load(rom);

        var output2 = new StringWriter();
        Console.SetOut(output2);

        for (var i = 0; i < 1_000_000; i++)
        {
            //output.WriteLine($"PC: {gameBoy.GetProcessor().GetValueOfRegisterPC():X4}");
            //output.WriteLine($"PC: {gameBoy.GetProcessor().GetValueOfRegisterPC():X4}");
            _ = gameBoy.Update();
            //Assert.NotEqual(0xFFFF, gameBoy.GetProcessor().GetValueOfRegisterPC());
        }

        var actualOutput = gameBoy.GetOutput();
        output.WriteLine(output2.ToString());
        output.WriteLine(actualOutput);
        
        Assert.Equal("HELLO", actualOutput);
    }
}