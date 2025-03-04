using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class DaaTests
{
    [Theory]
    [InlineData( 0x00, 0x00, 0x00, true, false)]
    [InlineData( 0x01, 0x00, 0x01, false, false)]
    [InlineData( 0x00, 0x01, 0x01, false, false)]
    [InlineData( 0x10, 0x01, 0x11, false, false)]
    [InlineData( 0x20, 0x20, 0x40, false, false)]
    [InlineData( 0x38, 0x45, 0x83, false, false)]
    [InlineData( 0x38, 0x41, 0x79, false, false)]
    [InlineData( 0x83, 0x54, 0x37, false, true)]
    [InlineData( 0x88, 0x44, 0x32, false, true)]
    [InlineData( 0x99, 0x01, 0x00, true, true)]
    public static void Daa_ResultCyclesAndFlagsAreSetAppropriately(byte value1, byte value2, byte expectedResult, bool expectedZeroFlag, bool expectedCarryFlag)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(value1, value2, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, false)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Add_A_B,
                [0x0001] = Opcode.Daa
            })
            .BuildGameBoy();

        gameBoy.Update();
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(4, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedZeroFlag, processor.ZeroFlagIsSet());
        Assert.False(processor.SubtractFlagIsSet());
        Assert.False(processor.HalfCarryFlagIsSet());
        Assert.Equal(expectedCarryFlag, processor.CarryFlagIsSet());
    }
}