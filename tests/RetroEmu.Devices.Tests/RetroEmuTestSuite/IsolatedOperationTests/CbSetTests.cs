using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class CbSetTests
{
    [Theory]
    [InlineData(CBOpcode.Set0_A, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_A, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_A, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_A, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_A, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_A, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_A, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_A, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_A, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_A, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_A, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_A, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_A, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_A, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_A, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_A, 0x00, 0x80)]
    public static void CBOperation_SetNA_SpecifictBitNSet(byte opcode, byte registerA, byte expectedRegisterA)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(registerA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterA, processor.GetValueOfRegisterA());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_B, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_B, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_B, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_B, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_B, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_B, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_B, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_B, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_B, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_B, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_B, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_B, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_B, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_B, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_B, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_B, 0x00, 0x80)]
    public static void CBOperation_SetNB_SpecifictBitNSet(byte opcode, byte registerB, byte expectedRegisterB)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, registerB, 0x00, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterB, processor.GetValueOfRegisterB());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_C, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_C, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_C, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_C, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_C, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_C, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_C, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_C, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_C, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_C, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_C, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_C, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_C, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_C, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_C, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_C, 0x00, 0x80)]
    public static void CBOperation_SetNC_SpecifictBitNSet(byte opcode, byte registerC, byte expectedRegisterC)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, registerC, 0x00, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterC, processor.GetValueOfRegisterC());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_D, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_D, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_D, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_D, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_D, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_D, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_D, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_D, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_D, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_D, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_D, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_D, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_D, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_D, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_D, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_D, 0x00, 0x80)]
    public static void CBOperation_SetND_SpecifictBitNSet(byte opcode, byte registerD, byte expectedRegisterD)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, registerD, 0x00, 0x00, 0x00)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterD, processor.GetValueOfRegisterD());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_E, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_E, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_E, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_E, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_E, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_E, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_E, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_E, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_E, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_E, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_E, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_E, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_E, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_E, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_E, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_E, 0x00, 0x80)]
    public static void CBOperation_SetNE_SpecifictBitNSet(byte opcode, byte registerE, byte expectedRegisterE)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, registerE, 0x00, 0x00)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterE, processor.GetValueOfRegisterE());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_H, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_H, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_H, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_H, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_H, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_H, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_H, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_H, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_H, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_H, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_H, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_H, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_H, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_H, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_H, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_H, 0x00, 0x80)]
    public static void CBOperation_SetNH_SpecifictBitNSet(byte opcode, byte registerH, byte expectedRegisterH)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, registerH, 0x00)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterH, processor.GetValueOfRegisterH());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_L, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_L, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_L, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_L, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_L, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_L, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_L, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_L, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_L, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_L, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_L, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_L, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_L, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_L, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_L, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_L, 0x00, 0x80)]
    public static void CBOperation_SetNL_SpecifictBitNSet(byte opcode, byte registerL, byte expectedRegisterL)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, registerL)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(8, cycles);
        Assert.Equal(expectedRegisterL, processor.GetValueOfRegisterL());
    }
    
    [Theory]
    [InlineData(CBOpcode.Set0_XHL, 0x01, 0x01)]
    [InlineData(CBOpcode.Set0_XHL, 0x00, 0x01)]
    [InlineData(CBOpcode.Set1_XHL, 0x02, 0x02)]
    [InlineData(CBOpcode.Set1_XHL, 0x00, 0x02)]
    [InlineData(CBOpcode.Set2_XHL, 0x04, 0x04)]
    [InlineData(CBOpcode.Set2_XHL, 0x00, 0x04)]
    [InlineData(CBOpcode.Set3_XHL, 0x08, 0x08)]
    [InlineData(CBOpcode.Set3_XHL, 0x00, 0x08)]
    [InlineData(CBOpcode.Set4_XHL, 0x10, 0x10)]
    [InlineData(CBOpcode.Set4_XHL, 0x00, 0x10)]
    [InlineData(CBOpcode.Set5_XHL, 0x20, 0x20)]
    [InlineData(CBOpcode.Set5_XHL, 0x00, 0x20)]
    [InlineData(CBOpcode.Set6_XHL, 0x40, 0x40)]
    [InlineData(CBOpcode.Set6_XHL, 0x00, 0x40)]
    [InlineData(CBOpcode.Set7_XHL, 0x80, 0x80)]
    [InlineData(CBOpcode.Set7_XHL, 0x00, 0x80)]
    public static void CBOperation_SetNXHL_SpecifictBitNSet(byte opcode, byte input, byte expectedResult)
    {
        const ushort hl = 0xC234;
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, hl, 0)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0000] = Opcode.Pre_CB,
                [0x0001] = opcode,
                [0x0002] = Opcode.Ld_A_XHL,
                [hl] = input
            })
            .BuildGameBoy();
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        
        var cycles = 0;
        while(processor.GetValueOfRegisterPC() < 0x0003) 
            cycles += gameBoy.Update();
        
        Assert.Equal(24, cycles);
        Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
    }
}