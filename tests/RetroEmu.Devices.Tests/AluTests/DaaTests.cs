using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.AluTests;

public class DaaTests
{
    [Theory]
    [InlineData( 1, 4, 0b0000_0001, false, false)]
    [InlineData( 9, 4, 0b0000_1001, false, false)]
    [InlineData( 10, 4, 0b0001_0000, false, false)]
    [InlineData( 11, 4, 0b0001_0001, false, false)]
    [InlineData( 55, 4, 0b0101_0101, false, false)]
    [InlineData( 99, 4, 0b1001_1001, false, false)]
    [InlineData( 100, 4, 0b0000_0000, true, true)]
    [InlineData( 255, 4, 0b0101_0101, false, true)]
    public static void Daa_ResultCyclesAndFlagsAreSetAppropriately(byte value, byte expectedCycles, byte expectedResult, bool expectedZeroFlag, bool expectedCarryFlag)
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
                [0x0001] = Opcode.Daa
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedZeroFlag, processor.IsSet(Flag.Zero));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.Equal(expectedCarryFlag, processor.IsSet(Flag.Carry));
    }
}