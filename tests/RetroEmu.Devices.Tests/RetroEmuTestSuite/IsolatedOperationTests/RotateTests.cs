using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class RotateTests
{
    [Theory]
    [InlineData(Opcode.Rlc_A, 0b11110000, false, 0b11100001, true)]
    [InlineData(Opcode.Rlc_A, 0b00000000, true, 0b00000000, false)]
    [InlineData(Opcode.Rla, 0b11110000, false, 0b11100000, true)]
    [InlineData(Opcode.Rla, 0b00000000, true, 0b00000001, false)]
    [InlineData(Opcode.Rrc_A, 0b00001111, false, 0b10000111, true)]
    [InlineData(Opcode.Rrc_A, 0b00000000, true, 0b00000000, false)]
    [InlineData(Opcode.Rra, 0b00001111, false, 0b00000111, true)]
    [InlineData(Opcode.Rra, 0b00000000, true, 0b10000000, false)]
    public static void WithAnyRotateOpcode_RotateAWithInputValue_ResultCarryAndZeroIsExpected(
        byte opcode, byte input, bool inputCarry, byte expectedResult, bool expectedCarry)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(input, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
            })
            .BuildGameBoy();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        if (inputCarry)
        {
            processor.SetCarryFlag();
        }

        var cycles = gameBoy.Update();

        Assert.Equal(4, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedCarry, processor.CarryFlagIsSet());
        Assert.False(processor.HalfCarryFlagIsSet());
        Assert.False(processor.SubtractFlagIsSet());
        Assert.False(processor.ZeroFlagIsSet());
    }
}