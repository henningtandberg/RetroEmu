using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class CbResTests
{
    [Theory]
    [InlineData(CBOpcode.Res0_A, 0x01)]
    [InlineData(CBOpcode.Res0_A, 0x00)]
    [InlineData(CBOpcode.Res1_A, 0x02)]
    [InlineData(CBOpcode.Res1_A, 0x00)]
    [InlineData(CBOpcode.Res2_A, 0x04)]
    [InlineData(CBOpcode.Res2_A, 0x00)]
    [InlineData(CBOpcode.Res3_A, 0x08)]
    [InlineData(CBOpcode.Res3_A, 0x00)]
    [InlineData(CBOpcode.Res4_A, 0x10)]
    [InlineData(CBOpcode.Res4_A, 0x00)]
    [InlineData(CBOpcode.Res5_A, 0x20)]
    [InlineData(CBOpcode.Res5_A, 0x00)]
    [InlineData(CBOpcode.Res6_A, 0x40)]
    [InlineData(CBOpcode.Res6_A, 0x00)]
    [InlineData(CBOpcode.Res7_A, 0x80)]
    [InlineData(CBOpcode.Res7_A, 0x00)]
    public static void CBOperation_ResNA_ZeroFlagIsSetCorrectly(byte opcode, byte registerA)
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
        Assert.Equal(0, processor.GetValueOfRegisterA());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_B, 0x01)]
    [InlineData(CBOpcode.Res0_B, 0x00)]
    [InlineData(CBOpcode.Res1_B, 0x02)]
    [InlineData(CBOpcode.Res1_B, 0x00)]
    [InlineData(CBOpcode.Res2_B, 0x04)]
    [InlineData(CBOpcode.Res2_B, 0x00)]
    [InlineData(CBOpcode.Res3_B, 0x08)]
    [InlineData(CBOpcode.Res3_B, 0x00)]
    [InlineData(CBOpcode.Res4_B, 0x10)]
    [InlineData(CBOpcode.Res4_B, 0x00)]
    [InlineData(CBOpcode.Res5_B, 0x20)]
    [InlineData(CBOpcode.Res5_B, 0x00)]
    [InlineData(CBOpcode.Res6_B, 0x40)]
    [InlineData(CBOpcode.Res6_B, 0x00)]
    [InlineData(CBOpcode.Res7_B, 0x80)]
    [InlineData(CBOpcode.Res7_B, 0x00)]
    public static void CBOperation_ResNB_ZeroFlagIsSetCorrectly(byte opcode, byte registerB)
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
        Assert.Equal(0, processor.GetValueOfRegisterB());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_C, 0x01)]
    [InlineData(CBOpcode.Res0_C, 0x00)]
    [InlineData(CBOpcode.Res1_C, 0x02)]
    [InlineData(CBOpcode.Res1_C, 0x00)]
    [InlineData(CBOpcode.Res2_C, 0x04)]
    [InlineData(CBOpcode.Res2_C, 0x00)]
    [InlineData(CBOpcode.Res3_C, 0x08)]
    [InlineData(CBOpcode.Res3_C, 0x00)]
    [InlineData(CBOpcode.Res4_C, 0x10)]
    [InlineData(CBOpcode.Res4_C, 0x00)]
    [InlineData(CBOpcode.Res5_C, 0x20)]
    [InlineData(CBOpcode.Res5_C, 0x00)]
    [InlineData(CBOpcode.Res6_C, 0x40)]
    [InlineData(CBOpcode.Res6_C, 0x00)]
    [InlineData(CBOpcode.Res7_C, 0x80)]
    [InlineData(CBOpcode.Res7_C, 0x00)]
    public static void CBOperation_ResNC_ZeroFlagIsSetCorrectly(byte opcode, byte registerC)
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
        Assert.Equal(0, processor.GetValueOfRegisterC());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_D, 0x01)]
    [InlineData(CBOpcode.Res0_D, 0x00)]
    [InlineData(CBOpcode.Res1_D, 0x02)]
    [InlineData(CBOpcode.Res1_D, 0x00)]
    [InlineData(CBOpcode.Res2_D, 0x04)]
    [InlineData(CBOpcode.Res2_D, 0x00)]
    [InlineData(CBOpcode.Res3_D, 0x08)]
    [InlineData(CBOpcode.Res3_D, 0x00)]
    [InlineData(CBOpcode.Res4_D, 0x10)]
    [InlineData(CBOpcode.Res4_D, 0x00)]
    [InlineData(CBOpcode.Res5_D, 0x20)]
    [InlineData(CBOpcode.Res5_D, 0x00)]
    [InlineData(CBOpcode.Res6_D, 0x40)]
    [InlineData(CBOpcode.Res6_D, 0x00)]
    [InlineData(CBOpcode.Res7_D, 0x80)]
    [InlineData(CBOpcode.Res7_D, 0x00)]
    public static void CBOperation_ResND_ZeroFlagIsSetCorrectly(byte opcode, byte registerD)
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
        Assert.Equal(0, processor.GetValueOfRegisterD());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_E, 0x01)]
    [InlineData(CBOpcode.Res0_E, 0x00)]
    [InlineData(CBOpcode.Res1_E, 0x02)]
    [InlineData(CBOpcode.Res1_E, 0x00)]
    [InlineData(CBOpcode.Res2_E, 0x04)]
    [InlineData(CBOpcode.Res2_E, 0x00)]
    [InlineData(CBOpcode.Res3_E, 0x08)]
    [InlineData(CBOpcode.Res3_E, 0x00)]
    [InlineData(CBOpcode.Res4_E, 0x10)]
    [InlineData(CBOpcode.Res4_E, 0x00)]
    [InlineData(CBOpcode.Res5_E, 0x20)]
    [InlineData(CBOpcode.Res5_E, 0x00)]
    [InlineData(CBOpcode.Res6_E, 0x40)]
    [InlineData(CBOpcode.Res6_E, 0x00)]
    [InlineData(CBOpcode.Res7_E, 0x80)]
    [InlineData(CBOpcode.Res7_E, 0x00)]
    public static void CBOperation_ResNE_ZeroFlagIsSetCorrectly(byte opcode, byte registerE)
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
        Assert.Equal(0, processor.GetValueOfRegisterE());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_H, 0x01)]
    [InlineData(CBOpcode.Res0_H, 0x00)]
    [InlineData(CBOpcode.Res1_H, 0x02)]
    [InlineData(CBOpcode.Res1_H, 0x00)]
    [InlineData(CBOpcode.Res2_H, 0x04)]
    [InlineData(CBOpcode.Res2_H, 0x00)]
    [InlineData(CBOpcode.Res3_H, 0x08)]
    [InlineData(CBOpcode.Res3_H, 0x00)]
    [InlineData(CBOpcode.Res4_H, 0x10)]
    [InlineData(CBOpcode.Res4_H, 0x00)]
    [InlineData(CBOpcode.Res5_H, 0x20)]
    [InlineData(CBOpcode.Res5_H, 0x00)]
    [InlineData(CBOpcode.Res6_H, 0x40)]
    [InlineData(CBOpcode.Res6_H, 0x00)]
    [InlineData(CBOpcode.Res7_H, 0x80)]
    [InlineData(CBOpcode.Res7_H, 0x00)]
    public static void CBOperation_ResNH_ZeroFlagIsSetCorrectly(byte opcode, byte registerH)
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
        Assert.Equal(0, processor.GetValueOfRegisterH());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_L, 0x01)]
    [InlineData(CBOpcode.Res0_L, 0x00)]
    [InlineData(CBOpcode.Res1_L, 0x02)]
    [InlineData(CBOpcode.Res1_L, 0x00)]
    [InlineData(CBOpcode.Res2_L, 0x04)]
    [InlineData(CBOpcode.Res2_L, 0x00)]
    [InlineData(CBOpcode.Res3_L, 0x08)]
    [InlineData(CBOpcode.Res3_L, 0x00)]
    [InlineData(CBOpcode.Res4_L, 0x10)]
    [InlineData(CBOpcode.Res4_L, 0x00)]
    [InlineData(CBOpcode.Res5_L, 0x20)]
    [InlineData(CBOpcode.Res5_L, 0x00)]
    [InlineData(CBOpcode.Res6_L, 0x40)]
    [InlineData(CBOpcode.Res6_L, 0x00)]
    [InlineData(CBOpcode.Res7_L, 0x80)]
    [InlineData(CBOpcode.Res7_L, 0x00)]
    public static void CBOperation_ResNL_ZeroFlagIsSetCorrectly(byte opcode, byte registerL)
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
        Assert.Equal(0, processor.GetValueOfRegisterL());
    }
    
    [Theory]
    [InlineData(CBOpcode.Res0_XHL, 0x01)]
    [InlineData(CBOpcode.Res0_XHL, 0x00)]
    [InlineData(CBOpcode.Res1_XHL, 0x02)]
    [InlineData(CBOpcode.Res1_XHL, 0x00)]
    [InlineData(CBOpcode.Res2_XHL, 0x04)]
    [InlineData(CBOpcode.Res2_XHL, 0x00)]
    [InlineData(CBOpcode.Res3_XHL, 0x08)]
    [InlineData(CBOpcode.Res3_XHL, 0x00)]
    [InlineData(CBOpcode.Res4_XHL, 0x10)]
    [InlineData(CBOpcode.Res4_XHL, 0x00)]
    [InlineData(CBOpcode.Res5_XHL, 0x20)]
    [InlineData(CBOpcode.Res5_XHL, 0x00)]
    [InlineData(CBOpcode.Res6_XHL, 0x40)]
    [InlineData(CBOpcode.Res6_XHL, 0x00)]
    [InlineData(CBOpcode.Res7_XHL, 0x80)]
    [InlineData(CBOpcode.Res7_XHL, 0x00)]
    public static void CBOperation_ResNXHL_ZeroFlagIsSetCorrectly(byte opcode, byte input)
    {
        const ushort hl = 0x1234;
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
        Assert.Equal(0, processor.GetValueOfRegisterA());
    }
}