using System;
using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class CbRotateTests
{
    [Theory]
    [InlineData(ProgramToRun.RR, 0b00001111, false, 56, 0b00000111, true, false)]
    [InlineData(ProgramToRun.RR, 0b00001111, true, 56, 0b10000111, true, false)]
    [InlineData(ProgramToRun.RR, 0b00000000, true, 56, 0b10000000, false, false)]
    [InlineData(ProgramToRun.RR, 0b00000000, false, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.RL, 0b11110000, false, 56, 0b11100000, true, false)]
    [InlineData(ProgramToRun.RL, 0b11110000, true, 56, 0b11100001, true, false)]
    [InlineData(ProgramToRun.RL, 0b00000000, true, 56, 0b00000001, false, false)]
    [InlineData(ProgramToRun.RL, 0b00000000, false, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.RRC, 0b00001111, false, 56, 0b10000111, true, false)]
    [InlineData(ProgramToRun.RRC, 0b00000000, true, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.RRC, 0b00000000, false, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.RLC, 0b11110000, false, 56, 0b11100001, true, false)]
    [InlineData(ProgramToRun.RLC, 0b00000000, true, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.RLC, 0b00000000, false, 56, 0b00000000, false, true)]
    public static void CBRotate8BitRegisterProgram_ResultCyclesCarryAndZeroIsSetExpected(
            ProgramToRun programToRun, byte input, bool carryFlag, int expectedCycles, byte expectedResult, bool expectedCarry, bool expectedZero)
    {
        var program = GetRotateProgram(programToRun);
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(a: input, b: input, c: input, d: input, e: input, h: input, l: input)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => program)
            .BuildGameBoy();

        var cycles = 0;
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        while (processor.GetValueOfRegisterPC() < program.Keys.Count)
        {
            processor.SetCarryFlagToValue(carryFlag);
            cycles += gameBoy.Update();
            
            Assert.Equal(expectedCarry, processor.CarryFlagIsSet());
            Assert.False(processor.HalfCarryFlagIsSet());
            Assert.False(processor.SubtractFlagIsSet());
            Assert.Equal(expectedZero, processor.ZeroFlagIsSet());
        }

        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.Equal(expectedResult, processor.GetValueOfRegisterB());
        Assert.Equal(expectedResult, processor.GetValueOfRegisterC());
        Assert.Equal(expectedResult, processor.GetValueOfRegisterD());
        Assert.Equal(expectedResult, processor.GetValueOfRegisterE());
        Assert.Equal(expectedResult, processor.GetValueOfRegisterH());
        Assert.Equal(expectedResult, processor.GetValueOfRegisterL());
    }

    public enum ProgramToRun
    {
        RR = 0,
        RL = 1,
        RRC = 2,
        RLC = 3
    }

    private static IDictionary<ushort, byte> GetRotateProgram(ProgramToRun programToRun) => programToRun switch
    {
        ProgramToRun.RR => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Rr_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Rr_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Rr_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Rr_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Rr_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Rr_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Rr_L
        },
        ProgramToRun.RL => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Rl_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Rl_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Rl_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Rl_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Rl_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Rl_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Rl_L
        },
        ProgramToRun.RRC => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Rrc_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Rrc_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Rrc_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Rrc_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Rrc_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Rrc_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Rrc_L
        },
        ProgramToRun.RLC => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Rlc_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Rlc_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Rlc_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Rlc_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Rlc_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Rlc_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Rlc_L
        },
        _ => throw new NotImplementedException()
    };
}