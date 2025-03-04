using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.IsolatedOperationTests;

public class CbBitTests
{
    [Theory]
    [InlineData(0b11111111, false)]
    [InlineData(0b00000000, true)]
    public static void CBOperation_BitNr8_ZeroFlagIsSetCorrectly(byte input, bool expectedZeroFlag)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(input, input, input, input, input, input, input)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(CreateBitNr8Program)
            .BuildGameBoy();

        var cycles = 0;
        var processor = (ITestableProcessor)(ITestableProcessor)gameBoy.GetProcessor();

        while (processor.GetValueOfRegisterPC() < 0x0070)
        {
            processor.SetFlags(!expectedZeroFlag, true, false, false);
            cycles += gameBoy.Update();
            
            Assert.Equal(expectedZeroFlag, processor.ZeroFlagIsSet());
            Assert.False(processor.SubtractFlagIsSet());   // Reset
            Assert.True(processor.HalfCarryFlagIsSet());   // Set
            Assert.False(processor.CarryFlagIsSet());      // Not affected
        }
        
        Assert.Equal(448, cycles);
    }

    private static IDictionary<ushort, byte> CreateBitNr8Program() => new Dictionary<ushort, byte>
    {
        // Bit 0
        [0x0000] = Opcode.Pre_CB,
        [0x0001] = CBOpcode.Bit0_A,
        [0x0002] = Opcode.Pre_CB,
        [0x0003] = CBOpcode.Bit0_B,
        [0x0004] = Opcode.Pre_CB,
        [0x0005] = CBOpcode.Bit0_C,
        [0x0006] = Opcode.Pre_CB,
        [0x0007] = CBOpcode.Bit0_D,
        [0x0008] = Opcode.Pre_CB,
        [0x0009] = CBOpcode.Bit0_E,
        [0x000A] = Opcode.Pre_CB,
        [0x000B] = CBOpcode.Bit0_H,
        [0x000C] = Opcode.Pre_CB,
        [0x000D] = CBOpcode.Bit0_L,
        // Bit 1
        [0x000E] = Opcode.Pre_CB,
        [0x000F] = CBOpcode.Bit1_A,
        [0x0010] = Opcode.Pre_CB,
        [0x0011] = CBOpcode.Bit1_B,
        [0x0012] = Opcode.Pre_CB,
        [0x0013] = CBOpcode.Bit1_C,
        [0x0014] = Opcode.Pre_CB,
        [0x0015] = CBOpcode.Bit1_D,
        [0x0016] = Opcode.Pre_CB,
        [0x0017] = CBOpcode.Bit1_E,
        [0x0018] = Opcode.Pre_CB,
        [0x0019] = CBOpcode.Bit1_H,
        [0x001A] = Opcode.Pre_CB,
        [0x001B] = CBOpcode.Bit1_L,
        // Bit 2
        [0x001C] = Opcode.Pre_CB,
        [0x001D] = CBOpcode.Bit2_A,
        [0x001E] = Opcode.Pre_CB,
        [0x001F] = CBOpcode.Bit2_B,
        [0x0020] = Opcode.Pre_CB,
        [0x0021] = CBOpcode.Bit2_C,
        [0x0022] = Opcode.Pre_CB,
        [0x0023] = CBOpcode.Bit2_D,
        [0x0024] = Opcode.Pre_CB,
        [0x0025] = CBOpcode.Bit2_E,
        [0x0026] = Opcode.Pre_CB,
        [0x0027] = CBOpcode.Bit2_H,
        [0x0028] = Opcode.Pre_CB,
        [0x0029] = CBOpcode.Bit2_L,
        // Bit 3
        [0x002A] = Opcode.Pre_CB,
        [0x002B] = CBOpcode.Bit3_A,
        [0x002C] = Opcode.Pre_CB,
        [0x002D] = CBOpcode.Bit3_B,
        [0x002E] = Opcode.Pre_CB,
        [0x002F] = CBOpcode.Bit3_C,
        [0x0030] = Opcode.Pre_CB,
        [0x0031] = CBOpcode.Bit3_D,
        [0x0032] = Opcode.Pre_CB,
        [0x0033] = CBOpcode.Bit3_E,
        [0x0034] = Opcode.Pre_CB,
        [0x0035] = CBOpcode.Bit3_H,
        [0x0036] = Opcode.Pre_CB,
        [0x0037] = CBOpcode.Bit3_L,
        // Bit 4
        [0x0038] = Opcode.Pre_CB,
        [0x0039] = CBOpcode.Bit4_A,
        [0x003A] = Opcode.Pre_CB,
        [0x003B] = CBOpcode.Bit4_B,
        [0x003C] = Opcode.Pre_CB,
        [0x003D] = CBOpcode.Bit4_C,
        [0x003E] = Opcode.Pre_CB,
        [0x003F] = CBOpcode.Bit4_D,
        [0x0040] = Opcode.Pre_CB,
        [0x0041] = CBOpcode.Bit4_E,
        [0x0042] = Opcode.Pre_CB,
        [0x0043] = CBOpcode.Bit4_H,
        [0x0044] = Opcode.Pre_CB,
        [0x0045] = CBOpcode.Bit4_L,
        // Bit 5
        [0x0046] = Opcode.Pre_CB,
        [0x0047] = CBOpcode.Bit5_A,
        [0x0048] = Opcode.Pre_CB,
        [0x0049] = CBOpcode.Bit5_B,
        [0x004A] = Opcode.Pre_CB,
        [0x004B] = CBOpcode.Bit5_C,
        [0x004C] = Opcode.Pre_CB,
        [0x004D] = CBOpcode.Bit5_D,
        [0x004E] = Opcode.Pre_CB,
        [0x004F] = CBOpcode.Bit5_E,
        [0x0050] = Opcode.Pre_CB,
        [0x0051] = CBOpcode.Bit5_H,
        [0x0052] = Opcode.Pre_CB,
        [0x0053] = CBOpcode.Bit5_L,
        // Bit 6
        [0x0054] = Opcode.Pre_CB,
        [0x0055] = CBOpcode.Bit6_A,
        [0x0056] = Opcode.Pre_CB,
        [0x0057] = CBOpcode.Bit6_B,
        [0x0058] = Opcode.Pre_CB,
        [0x0059] = CBOpcode.Bit6_C,
        [0x005A] = Opcode.Pre_CB,
        [0x005B] = CBOpcode.Bit6_D,
        [0x005C] = Opcode.Pre_CB,
        [0x005D] = CBOpcode.Bit6_E,
        [0x005E] = Opcode.Pre_CB,
        [0x005F] = CBOpcode.Bit6_H,
        [0x0060] = Opcode.Pre_CB,
        [0x0061] = CBOpcode.Bit6_L,
        // Bit 7
        [0x0062] = Opcode.Pre_CB,
        [0x0063] = CBOpcode.Bit7_A,
        [0x0064] = Opcode.Pre_CB,
        [0x0065] = CBOpcode.Bit7_B,
        [0x0066] = Opcode.Pre_CB,
        [0x0067] = CBOpcode.Bit7_C,
        [0x0068] = Opcode.Pre_CB,
        [0x0069] = CBOpcode.Bit7_D,
        [0x006A] = Opcode.Pre_CB,
        [0x006B] = CBOpcode.Bit7_E,
        [0x006C] = Opcode.Pre_CB,
        [0x006D] = CBOpcode.Bit7_H,
        [0x006E] = Opcode.Pre_CB,
        [0x006F] = CBOpcode.Bit7_L
    };

    [Theory]
    [InlineData(0b11111111, false)]
    [InlineData(0b00000000, true)]
    public static void CBOperation_BitNXHL_ZeroFlagIsSetCorrectly(byte input, bool expectedZeroFlag)
    {
        const int hl = 0x1234;
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set16BitGeneralPurposeRegisters(0, 0, 0, hl, 0)
                .SetProgramCounter(0x0000)
            )
            .WithMemory(() => CreateBitNXHLProgram(input, hl))
            .BuildGameBoy();

        var cycles = 0;
        var processor = (ITestableProcessor)(ITestableProcessor)gameBoy.GetProcessor();

        while (processor.GetValueOfRegisterPC() < 0x0010)
        {
            processor.SetFlags(!expectedZeroFlag, true, false, false);
            
            cycles += gameBoy.Update();
            
            Assert.Equal(expectedZeroFlag, processor.ZeroFlagIsSet());
            Assert.False(processor.SubtractFlagIsSet());   // Reset
            Assert.True(processor.HalfCarryFlagIsSet());   // Set
            Assert.False(processor.CarryFlagIsSet());      // Not affected
        }
        
        Assert.Equal(128, cycles);
    }

    private static IDictionary<ushort, byte> CreateBitNXHLProgram(byte input, ushort hl) => new Dictionary<ushort, byte>
    {
        [0x0000] = Opcode.Pre_CB,
        [0x0001] = CBOpcode.Bit0_XHL,
        [0x0002] = Opcode.Pre_CB,
        [0x0003] = CBOpcode.Bit1_XHL,
        [0x0004] = Opcode.Pre_CB,
        [0x0005] = CBOpcode.Bit2_XHL,
        [0x0006] = Opcode.Pre_CB,
        [0x0007] = CBOpcode.Bit3_XHL,
        [0x0008] = Opcode.Pre_CB,
        [0x0009] = CBOpcode.Bit4_XHL,
        [0x000A] = Opcode.Pre_CB,
        [0x000B] = CBOpcode.Bit5_XHL,
        [0x000C] = Opcode.Pre_CB,
        [0x000D] = CBOpcode.Bit6_XHL,
        [0x000E] = Opcode.Pre_CB,
        [0x000F] = CBOpcode.Bit7_XHL,
        [hl] = input
    };
}