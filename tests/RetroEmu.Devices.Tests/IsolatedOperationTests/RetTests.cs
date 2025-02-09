using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class RetTests
{
    [Fact]
    public static void RetN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack()
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xDFFE))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Ret,
                [0xDFFE] = 0x34,
                [0xDFFF] = 0x12
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(0x1234, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, 0x1234, 20)]
    [InlineData(false, 0x0001, 8)]
    public static void RetCN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool carryFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xDFFE)
                .SetFlags(false, false, false, carryFlag))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.RetC,
                [0x0001] = Opcode.Nop,
                [0xDFFE] = 0x34,
                [0xDFFF] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, 0x1234, 20)]
    [InlineData(true, 0x0001, 8)]
    public static void RetNCN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool carryFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xDFFE)
                .SetFlags(false, false, false, carryFlag))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.RetNC,
                [0xDFFE] = 0x34,
                [0xDFFF] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, 0x1234, 20)]
    [InlineData(false, 0x0001, 8)]
    public static void RetZN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool zeroFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xDFFE)
                .SetFlags(zeroFlag, false, false, false))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.RetZ,
                [0xDFFE] = 0x34,
                [0xDFFF] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, 0x1234, 20)]
    [InlineData(true, 0x0001, 8)]
    public static void RetNZN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool zeroFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xDFFE)
                .SetFlags(zeroFlag, false, false, false))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.RetNZ,
                [0xDFFE] = 0x34,
                [0xDFFF] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
}