using System;
using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class CbShiftTests
{
    [Theory]
    [InlineData(ProgramToRun.SLA, 0b00001111, 56, 0b00011110, false, false)]
    [InlineData(ProgramToRun.SLA, 0b11110000, 56, 0b11100000, true, false)]
    [InlineData(ProgramToRun.SLA, 0b00000000, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.SRA, 0b00001111, 56, 0b00000111, true, false)]
    [InlineData(ProgramToRun.SRA, 0b11110000, 56, 0b11111000, false, false)]
    [InlineData(ProgramToRun.SRA, 0b00000000, 56, 0b00000000, false, true)]
    [InlineData(ProgramToRun.SRL, 0b00001111, 56, 0b00000111, true, false)]
    [InlineData(ProgramToRun.SRL, 0b11110000, 56, 0b01111000, false, false)]
    [InlineData(ProgramToRun.SRL, 0b00000000, 56, 0b00000000, false, true)]
    public static void CBShiftRight8BitRegisterProgram_ResultCyclesCarryAndZeroIsSetExpected(
            ProgramToRun programToRun, byte input, int expectedCycles, byte expectedResult, bool expectedCarry, bool expectedZero)
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
            cycles += gameBoy.Update();
            
            Assert.Equal(expectedCarry, processor.GetValueOfCarryFlag());
            Assert.False(processor.GetValueOfHalfCarryFlag());
            Assert.False(processor.GetValueOfSubtractFlag());
            Assert.Equal(expectedZero, processor.GetValueOfZeroFlag());
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
        SLA = 0,
        SRA = 1,
        SRL = 2,
    }

    private static IDictionary<ushort, byte> GetRotateProgram(ProgramToRun programToRun) => programToRun switch
    {
        ProgramToRun.SLA => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Sla_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Sla_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Sla_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Sla_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Sla_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Sla_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Sla_L
        },
        ProgramToRun.SRA => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Sra_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Sra_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Sra_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Sra_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Sra_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Sra_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Sra_L
        },
        ProgramToRun.SRL => new Dictionary<ushort, byte>
        {
            [0x0001] = Opcode.Pre_CB,
            [0x0002] = CBOpcode.Srl_A,
            [0x0003] = Opcode.Pre_CB,
            [0x0004] = CBOpcode.Srl_B,
            [0x0005] = Opcode.Pre_CB,
            [0x0006] = CBOpcode.Srl_C,
            [0x0007] = Opcode.Pre_CB,
            [0x0008] = CBOpcode.Srl_D,
            [0x0009] = Opcode.Pre_CB,
            [0x000A] = CBOpcode.Srl_E,
            [0x000B] = Opcode.Pre_CB,
            [0x000C] = CBOpcode.Srl_H,
            [0x000D] = Opcode.Pre_CB,
            [0x000E] = CBOpcode.Srl_L
        },
        _ => throw new NotImplementedException()
    };
}