using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class JumpTests
{
    [Fact]
    public static void JpN16_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned()
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Jp_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(16, cycles);
        Assert.Equal(0x1234, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, 0x1234, 16)]
    [InlineData(false, 0x0003, 12)]
    public static void JpCN16_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool carryFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(false, false, false, carryFlag)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JpC_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, 0x1234, 16)]
    [InlineData(true, 0x0003, 12)]
    public static void JpNCN16_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool carryFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(false, false, false, carryFlag)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JpNC_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, 0x1234, 16)]
    [InlineData(false, 0x0003, 12)]
    public static void JpZN16_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool zeroFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(zeroFlag, false, false, false)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JpZ_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, 0x1234, 16)]
    [InlineData(true, 0x0003, 12)]
    public static void JpNZN16_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool zeroFlag, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(zeroFlag, false, false, false)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JpNZ_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(-1, 0x0001)]
    [InlineData(1, 0x0003)]
    public static void JrN8_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(sbyte relativeJump, ushort expectedProgramCounter)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Jr_N8,
                [0x0001] = (byte)relativeJump
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(12, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, -1,0x0002, 16)]
    [InlineData(true, 1,0x0004, 16)]
    [InlineData(false, 1, 0x0003, 12)]
    public static void JrCN8_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool carryFlag, sbyte relativeJump, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(false, false, false, carryFlag)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JrC_N8,
                [0x0001] = (byte)relativeJump,
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, -1,0x0002, 16)]
    [InlineData(false, 1,0x0004, 16)]
    [InlineData(true, 1, 0x0003, 12)]
    public static void JrNCN8_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool carryFlag, sbyte relativeJump, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(false, false, false, carryFlag)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JrNC_N8,
                [0x0001] = (byte)relativeJump
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, -1,0x0002, 16)]
    [InlineData(true, 1,0x0004, 16)]
    [InlineData(false, 1, 0x0003, 12)]
    public static void JrZN8_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool zeroFlag, sbyte relativeJump, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(zeroFlag, false, false, false)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JrZ_N8,
                [0x0001] = (byte)relativeJump
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, -1,0x0002, 16)]
    [InlineData(false, 1,0x0004, 16)]
    [InlineData(true, 1, 0x0003, 12)]
    public static void JrNZN8_ProgramCounterIsUpdatedCorrectlyAndCorrectCyclesReturned(bool zeroFlag, sbyte relativeJump, ushort expectedProgramCounter, int expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .SetFlags(zeroFlag, false, false, false)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.JrNZ_N8,
                [0x0001] = (byte)relativeJump
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
}