using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.AluTests;

public class AdcTests
{
    [Theory]
    [InlineData(Opcode.Adc_A_B, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_C, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_D, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_E, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_H, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_L, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_XHL, 1, 1, 8, 2)]
    [InlineData(Opcode.Adc_A_A, 1, 1, 4, 2)]
    [InlineData(Opcode.Adc_A_N8, 1, 1, 8, 2)]
    public static void Adc_InstructionIsPerformedWithInputXYCausingNoOverflow_CyclesAndResultAreCorrectWithNoCarryFlag(
        byte opcode, byte valueX, byte valueY, byte expectedCycles, byte expectedSum)
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
                [0x0002] = valueX,
                [0x0101] = valueY
            })
            .BuildGameBoy();
            
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedSum, processor.GetValueOfRegisterA());
        Assert.False(processor.IsSet(Flag.Carry));
        Assert.False(processor.IsSet(Flag.HalfCarry));
        Assert.False(processor.IsSet(Flag.Subtract));
        Assert.False(processor.IsSet(Flag.Zero));
    }

    [Theory]
    [InlineData(Opcode.Adc_A_B, 1, 0xFF, 4, 0)]
    [InlineData(Opcode.Adc_A_C, 1, 0xFF, 4, 0)]
    [InlineData(Opcode.Adc_A_D, 1, 0xFF, 4, 0)]
    [InlineData(Opcode.Adc_A_E, 1, 0xFF, 4, 0)]
    [InlineData(Opcode.Adc_A_H, 1, 0xFF, 4, 0)]
    [InlineData(Opcode.Adc_A_L, 1, 0xFF, 4, 0)]
    [InlineData(Opcode.Adc_A_XHL, 1, 0xFF, 8, 0)]
    [InlineData(Opcode.Adc_A_A, 0xFF, 0xFF, 4, 0xFE)]
    [InlineData(Opcode.Adc_A_N8, 1, 0xFF, 8, 0)]
    public static void Adc_InstructionIsPerformedWithInputXYCausingOverflow_CyclesAndResultAreCorrectWithCarryFlagSet(
        byte opcode, byte valueX, byte valueY, byte expectedCycles, byte expectedSum)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(valueX, valueY, valueY, valueY, valueY, valueY, valueY)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = opcode,
                [0x0002] = valueY,
                [0xFFFF] = valueY
            })
            .BuildGameBoy();
            
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedSum, processor.GetValueOfRegisterA());
        Assert.True(processor.IsSet(Flag.Carry));
    }

    [Theory]
    [InlineData(Opcode.Adc_A_B, 0)]
    [InlineData(Opcode.Adc_A_C, 0)]
    [InlineData(Opcode.Adc_A_D, 0)]
    [InlineData(Opcode.Adc_A_E, 0)]
    [InlineData(Opcode.Adc_A_H, 0)]
    [InlineData(Opcode.Adc_A_L, 0)]
    [InlineData(Opcode.Adc_A_XHL, 0)]
    [InlineData(Opcode.Adc_A_A, 0)]
    [InlineData(Opcode.Adc_A_N8, 0)]
    public static unsafe void
        AnyAdcOpcode_InstructionIsPerformed_ResultIsZeroAndZeroFlagIsSet(byte opcode, byte expectedResult)
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
                [0x0000] = 0x00
            })
            .BuildGameBoy();
            
        _ = gameBoy.Update();

        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedResult, *processor.Registers.A);
        Assert.True(processor.IsSet(Flag.Zero));
    }

    [Theory]
    [InlineData(Opcode.Adc_A_B)]
    [InlineData(Opcode.Adc_A_C)]
    [InlineData(Opcode.Adc_A_D)]
    [InlineData(Opcode.Adc_A_E)]
    [InlineData(Opcode.Adc_A_H)]
    [InlineData(Opcode.Adc_A_L)]
    [InlineData(Opcode.Adc_A_XHL)]
    [InlineData(Opcode.Adc_A_A)]
    [InlineData(Opcode.Adc_A_N8)]
    public static void AnyAdcOpcode_InstructionIsPerformedWithHalfCarry_HalfCarryFlagIsSet(byte opcode)
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
            
        var processor = gameBoy.GetProcessor();
        Assert.True(processor.IsSet(Flag.HalfCarry));
    }
        
    [Theory]
    [InlineData(Opcode.Adc_A_B)]
    [InlineData(Opcode.Adc_A_C)]
    [InlineData(Opcode.Adc_A_D)]
    [InlineData(Opcode.Adc_A_E)]
    [InlineData(Opcode.Adc_A_H)]
    [InlineData(Opcode.Adc_A_L)]
    [InlineData(Opcode.Adc_A_XHL)]
    [InlineData(Opcode.Adc_A_A)]
    [InlineData(Opcode.Adc_A_N8)]
    public static void AnyAdcOpcode_InstructionIsPerformedWithCarry_CarryFlagIsSet(byte opcode)
    {
        // TODO: Fix issue with writing to illagal memory address
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
            
        var processor = gameBoy.GetProcessor();
        Assert.True(processor.IsSet(Flag.Carry));
    }
}