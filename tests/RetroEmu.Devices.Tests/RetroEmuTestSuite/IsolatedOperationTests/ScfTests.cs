using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class ScfTests
{
    [Theory]
    [InlineData( true, 4, true)]
    [InlineData( false, 4, true)]
    public static void Scf_CarryFlagIsSetAndExpectedCyclesAreCorrect(bool initialCarryFlag, byte expectedCycles, bool carryFlagIsSet)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, true, true, initialCarryFlag)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Scf
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.False(processor.ZeroFlagIsSet());
        Assert.False(processor.SubtractFlagIsSet());
        Assert.False(processor.HalfCarryFlagIsSet());
        Assert.Equal(carryFlagIsSet, processor.CarryFlagIsSet());
    }
}