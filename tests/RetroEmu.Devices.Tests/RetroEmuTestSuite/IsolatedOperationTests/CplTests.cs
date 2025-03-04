using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class CplTests
{
    [Theory]
    [InlineData( 0x01, 4, 0xFE)]
    [InlineData( 0xFE, 4, 0x01)]
    public static void Cpl_ResultCyclesAndFlagsAreSetAppropriately(byte value, byte expectedCycles, byte expectedResult)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(value, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, false)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Cpl
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.False(processor.ZeroFlagIsSet());
        Assert.True(processor.SubtractFlagIsSet());
        Assert.True(processor.HalfCarryFlagIsSet());
        Assert.False(processor.CarryFlagIsSet());
    }
}