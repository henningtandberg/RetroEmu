using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class AddTests
{
    [Theory]
    [InlineData(Opcode.Add_A_B, 4, 2)]
    [InlineData(Opcode.Add_A_C, 4, 2)]
    [InlineData(Opcode.Add_A_D, 4, 2)]
    [InlineData(Opcode.Add_A_E, 4, 2)]
    [InlineData(Opcode.Add_A_H, 4, 2)]
    [InlineData(Opcode.Add_A_L, 4, 2)]
    [InlineData(Opcode.Add_A_XHL, 8, 2)]
    [InlineData(Opcode.Add_A_A, 4, 2)]
    [InlineData(Opcode.Add_A_N8, 8, 2)]
    public static void WithAnyAddOpcode_AddInstructionIsPerformedWithNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
        byte opcode, byte expectedCycles, byte expectedResult)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
                [0x0002] = 0x01,
                [0x0101] = 0x01
            })
            .BuildGameBoy();
            
        var cycles = gameBoy.Update();
            
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.False(processor.CarryFlagIsSet());
        Assert.False(processor.HalfCarryFlagIsSet());
        Assert.False(processor.SubtractFlagIsSet());
        Assert.False(processor.ZeroFlagIsSet());
    }
        
    [Theory]
    [InlineData(Opcode.Add_A_B, 0)]
    [InlineData(Opcode.Add_A_C, 0)]
    [InlineData(Opcode.Add_A_D, 0)]
    [InlineData(Opcode.Add_A_E, 0)]
    [InlineData(Opcode.Add_A_H, 0)]
    [InlineData(Opcode.Add_A_L, 0)]
    [InlineData(Opcode.Add_A_XHL, 0)]
    [InlineData(Opcode.Add_A_A, 0)]
    [InlineData(Opcode.Add_A_N8, 0)]
    public static void WithAnyAddOpcode_AddInstructionIsPerformed_ResultIsZeroAndZeroFlagIsSet(
        byte opcode, byte expectedResult)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
                [0x0002] = 0x00,
                [0x8080] = 0x00
            })
            .BuildGameBoy();
            
        _ = gameBoy.Update();
            
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
        Assert.True(processor.ZeroFlagIsSet());
    }
        
    [Theory]
    [InlineData(Opcode.Add_A_B)]
    [InlineData(Opcode.Add_A_C)]
    [InlineData(Opcode.Add_A_D)]
    [InlineData(Opcode.Add_A_E)]
    [InlineData(Opcode.Add_A_H)]
    [InlineData(Opcode.Add_A_L)]
    [InlineData(Opcode.Add_A_XHL)]
    [InlineData(Opcode.Add_A_A)]
    [InlineData(Opcode.Add_A_N8)]
    public static void WithAnyAddOpcode_AddInstructionIsPerformedWithHalfCarry_HalfCarryFlagIsSet(byte opcode)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
                [0x0002] = 0x08,
                [0x0808] = 0x08
            })
            .BuildGameBoy();
            
        _ = gameBoy.Update();
            
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.True(processor.HalfCarryFlagIsSet());
    }
        
    [Theory]
    [InlineData(Opcode.Add_A_B)]
    [InlineData(Opcode.Add_A_C)]
    [InlineData(Opcode.Add_A_D)]
    [InlineData(Opcode.Add_A_E)]
    [InlineData(Opcode.Add_A_H)]
    [InlineData(Opcode.Add_A_L)]
    [InlineData(Opcode.Add_A_XHL)]
    [InlineData(Opcode.Add_A_A)]
    [InlineData(Opcode.Add_A_N8)]
    public static void WithAnyAddOpcode_AddInstructionIsPerformedWithCarry_CarryFlagIsSet(byte opcode)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
                [0x0002] = 0x80,
                [0x8080] = 0x80
            })
            .BuildGameBoy();
            
        _ = gameBoy.Update();
            
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.True(processor.CarryFlagIsSet());
    }

    [Theory]
    [InlineData(Opcode.Add_HL_BC, 8, 5, 7, 12, false, false)]
    [InlineData(Opcode.Add_HL_DE, 8, 5, 7, 12, false, false)]
    [InlineData(Opcode.Add_HL_HL, 8, 5, 5, 10, false, false)]
    [InlineData(Opcode.Add_HL_SP, 8, 5, 7, 12, false, false)]
    [InlineData(Opcode.Add_HL_BC, 8, 0x000F, 0x0001, 0x0010, false, false)]
    [InlineData(Opcode.Add_HL_BC, 8, 0x00FF, 0x0001, 0x0100, false, false)]
    [InlineData(Opcode.Add_HL_BC, 8, 0x0FFF, 0x0001, 0x1000, false, true)]
    [InlineData(Opcode.Add_HL_BC, 8, 0xFFFF, 0x0002, 0x0001, true, true)]
    public static void WithAnyAdd16Opcode_AddInstructionIsPerformedWithInputXYNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
        byte opcode, byte expectedCycles, ushort valueX, ushort valueY, ushort expectedResult, bool expectedCarry, bool expectedHalfCarry)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0x0000, valueY, valueY, valueX, valueY)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterHL());
        Assert.Equal(expectedCarry, processor.CarryFlagIsSet());
        Assert.Equal(expectedHalfCarry, processor.HalfCarryFlagIsSet());
        Assert.False(processor.SubtractFlagIsSet());
        Assert.False(processor.ZeroFlagIsSet());
    }

    [Theory]
    [InlineData(Opcode.Add_SP_N8, 0x000F, 1, 0x0010, false, true)]
    [InlineData(Opcode.Add_SP_N8, 0x000F, -1, 0x000E, true, true)]
    [InlineData(Opcode.Add_SP_N8, 0x00FF, 1, 0x0100, true, true)]
    [InlineData(Opcode.Add_SP_N8, 0x00FF, -1, 0x00FE, true, true)]
    public static void WithSPAdd16Opcode_AddInstructionIsPerformedWithInputXYNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
        byte opcode, ushort valueX, sbyte valueY, ushort expectedResult, bool expectedCarry, bool expectedHalfCarry)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0x0000, 0x0000, 0x0000, 0x0000, valueX)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
                [0x0002] = (byte)valueY
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(16, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterSP());
        Assert.Equal(expectedCarry, processor.CarryFlagIsSet());
        Assert.Equal(expectedHalfCarry, processor.HalfCarryFlagIsSet());
        Assert.False(processor.SubtractFlagIsSet());
        Assert.False(processor.ZeroFlagIsSet());
    }
}