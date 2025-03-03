using System;
using System.IO;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.Tests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.Devices.Tests.GBMicroTests;

public class GBMicroTests()
{
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .WithProcessor(processor =>
            processor.SetProgramCounter(0x0100))
        .BuildGameBoy();
    
    [Theory]
    [InlineData("GBMicroTest/Resources/oam_write_l0_a.gb")]
    public void RunMicroTest(String path)
    {
        var rom = File.ReadAllBytes(path);
        _gameBoy.Load(rom);

        var maxIterations = 200_000;
        for (var i = 0; i < maxIterations; i++)
        {
            _ = _gameBoy.Update();

            if (_gameBoy.GetMemory().Read(0xFF82) != 0)
            {
                break;
            }
        }

        Assert.Equal(0x01, _gameBoy.GetMemory().Read(0xFF82));
    }
}