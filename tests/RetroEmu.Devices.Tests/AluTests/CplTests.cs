using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.AluTests;

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
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.False(processor.IsSet(Flag.Zero));
        Assert.True(processor.IsSet(Flag.Subtract));
        Assert.True(processor.IsSet(Flag.HalfCarry));
        Assert.False(processor.IsSet(Flag.Carry));
    }
}