using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class CcfTests
{
    [Theory]
    [InlineData( true, 4, false)]
    [InlineData( false, 4, true)]
    public static void Ccf_CarryFlagIsComplementedAndExpectedCyclesAreCorrect(bool initialCarryFlag, byte expectedCycles, bool carryFlagIsSet)
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
                [0x0001] = Opcode.Ccf
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