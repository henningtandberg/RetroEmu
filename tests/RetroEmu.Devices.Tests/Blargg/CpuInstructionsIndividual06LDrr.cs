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

        for (var i = 0; i < 330_000; i++)
        {
            _ = gameBoy.Update();
        }

        var actualOutput = gameBoy.GetOutput();
        output.WriteLine(actualOutput);
        
        Assert.Equal("06-ld r,r\n\n\nPassed\n", actualOutput);
    }
}