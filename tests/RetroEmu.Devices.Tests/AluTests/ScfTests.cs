using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.AluTests;

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
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.False(processor.IsSet(Flag.Zero));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.Equal(carryFlagIsSet, processor.IsSet(Flag.Carry));
    }
}