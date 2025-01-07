using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class CbRotateTests
{
    [Theory]
    [InlineData(CBOpcode.Rr_A, 0b00001111, false, 0b00000111, true, false)]
    [InlineData(CBOpcode.Rr_A, 0b00001111, true, 0b10000111, true, false)]
    [InlineData(CBOpcode.Rr_A, 0b00000000, true, 0b10000000, false, false)]
    [InlineData(CBOpcode.Rr_A, 0b00000000, false, 0b00000000, false, true)]
    public static void CBRotateOperation_RotateARightThroughCarry_ResultCarryAndZeroIsSetExpected(
            byte opcode, byte input, bool carryFlag, byte expectedResult, bool expectedCarry, bool expectedZero)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(input, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, carryFlag)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Pre_CB,
                [0x0002] = opcode
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
    }
    
    [Theory]
    [InlineData(CBOpcode.Rl_A, 0b11110000, false, 0b11100000, true, false)]
    [InlineData(CBOpcode.Rl_A, 0b11110000, true, 0b11100001, true, false)]
    [InlineData(CBOpcode.Rl_A, 0b00000000, true, 0b00000001, false, false)]
    [InlineData(CBOpcode.Rl_A, 0b00000000, false, 0b00000000, false, true)]
    public static void CBRotateOperation_RotateALeftThroughCarry_ResultCarryAndZeroIsSetExpected(
            byte opcode, byte input, bool carryFlag, byte expectedResult, bool expectedCarry, bool expectedZero)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(input, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, carryFlag)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Pre_CB,
                [0x0002] = opcode
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
    }
    
    [Theory]
    [InlineData(CBOpcode.Rrc_A, 0b00001111, false, 0b10000111, true, false)]
    [InlineData(CBOpcode.Rrc_A, 0b00000000, true, 0b00000000, false, true)]
    [InlineData(CBOpcode.Rrc_A, 0b00000000, false, 0b00000000, false, true)]
    public static void CBRotateOperation_RotateARight_ResultCarryAndZeroIsSetExpected(
            byte opcode, byte input, bool carryFlag, byte expectedResult, bool expectedCarry, bool expectedZero)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(input, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, carryFlag)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Pre_CB,
                [0x0002] = opcode
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
    }
    
    [Theory]
    [InlineData(CBOpcode.Rlc_A, 0b11110000, false, 0b11100001, true, false)]
    [InlineData(CBOpcode.Rlc_A, 0b00000000, true, 0b00000000, false, true)]
    [InlineData(CBOpcode.Rlc_A, 0b00000000, false, 0b00000000, false, true)]
    public static void CBRotateOperation_RotateALeft_ResultCarryAndZeroIsSetExpected(
            byte opcode, byte input, bool carryFlag, byte expectedResult, bool expectedCarry, bool expectedZero)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(input, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetFlags(false, false, false, carryFlag)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Pre_CB,
                [0x0002] = opcode
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
    }
}