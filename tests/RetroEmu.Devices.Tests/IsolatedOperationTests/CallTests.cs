using System.Collections.Generic;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class CallTests
{
    [Fact]
    public static void CallN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack()
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xE000))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Call_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();

        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        var memory = gameBoy.GetMemory();
        AssertStackAndStackPointer(processor, memory, expectedStackPointer: 0xDFFE, expectedNextInstruction: 0x0003);
        Assert.Equal(24, cycles);
        Assert.Equal(0x1234, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, 0x1234, 24, true)]
    [InlineData(false, 0x0003, 12, false)]
    public static void CallCN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool carryFlag, ushort expectedProgramCounter, int expectedCycles, bool nextInstructionIsPushedToStack)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xE000) // Dette er et problem fordi AF = 0 setter alle flagg til false
                .SetFlags(false, false, false, carryFlag)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.CallC_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        if (nextInstructionIsPushedToStack)
        {
            var memory = gameBoy.GetMemory();
            AssertStackAndStackPointer(processor, memory, expectedStackPointer: 0xDFFE, expectedNextInstruction: 0x0003);
        }
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, 0x1234, 24, true)]
    [InlineData(true, 0x0003, 12, false)]
    public static void CallNCN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool carryFlag, ushort expectedProgramCounter, int expectedCycles, bool nextInstructionIsPushedToStack)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xE000) // Dette er et problem fordi AF = 0 setter alle flagg til false
                .SetFlags(false, false, false, carryFlag)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.CallNC_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        if (nextInstructionIsPushedToStack)
        {
            var memory = gameBoy.GetMemory();
            AssertStackAndStackPointer(processor, memory, expectedStackPointer: 0xDFFE, expectedNextInstruction: 0x0003);
        }
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(true, 0x1234, 24, true)]
    [InlineData(false, 0x0003, 12, false)]
    public static void CallZN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool zeroFlag, ushort expectedProgramCounter, int expectedCycles, bool nextInstructionIsPushedToStack)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xE000) // Dette er et problem fordi AF = 0 setter alle flagg til false
                .SetFlags(zeroFlag, false, false, false)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.CallZ_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        if (nextInstructionIsPushedToStack)
        {
            var memory = gameBoy.GetMemory();
            AssertStackAndStackPointer(processor, memory, expectedStackPointer: 0xDFFE, expectedNextInstruction: 0x0003);
        }
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    [Theory]
    [InlineData(false, 0x1234, 24, true)]
    [InlineData(true, 0x0003, 12, false)]
    public static void CallNZN16_ProgramCounterIsUpdatedCorrectlyCorrectCyclesReturnedAndNextInstructionIsPushedToStack(bool zeroFlag, ushort expectedProgramCounter, int expectedCycles, bool nextInstructionIsPushedToStack)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, 0, 0xE000) // Dette er et problem fordi AF = 0 setter alle flagg til false
                .SetFlags(zeroFlag, false, false, false)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.CallNZ_N16,
                [0x0001] = 0x34,
                [0x0002] = 0x12
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = gameBoy.GetProcessor();
        if (nextInstructionIsPushedToStack)
        {
            var memory = gameBoy.GetMemory();
            AssertStackAndStackPointer(processor, memory, expectedStackPointer: 0xDFFE, expectedNextInstruction: 0x0003);
        }
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedProgramCounter, processor.GetValueOfRegisterPC());
    }
    
    private static void AssertStackAndStackPointer(IProcessor processor, IMemory memory, ushort expectedStackPointer, ushort expectedNextInstruction)
    {
        var sp = processor.GetValueOfRegisterSP();
        ushort addressOfNextInstruction = memory.Read((ushort)(sp + 1));
        addressOfNextInstruction <<= 8;
        addressOfNextInstruction |= memory.Read((ushort)(sp + 0));
        
        Assert.Equal(expectedStackPointer, sp);
        Assert.Equal(expectedNextInstruction, addressOfNextInstruction);
    }
}