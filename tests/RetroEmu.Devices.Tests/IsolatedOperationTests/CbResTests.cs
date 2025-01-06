using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests;

public class CbResTests
{
    [Theory]
    [InlineData(CBOpcode.Res0_A, 0x01, 0x00, 8)]
    [InlineData(CBOpcode.Res0_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res1_A, 0x02, 0x00, 8)]
    [InlineData(CBOpcode.Res1_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res2_A, 0x04, 0x00, 8)]
    [InlineData(CBOpcode.Res2_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res3_A, 0x08, 0x00, 8)]
    [InlineData(CBOpcode.Res3_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res4_A, 0x10, 0x00, 8)]
    [InlineData(CBOpcode.Res4_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res5_A, 0x20, 0x00, 8)]
    [InlineData(CBOpcode.Res5_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res6_A, 0x40, 0x00, 8)]
    [InlineData(CBOpcode.Res6_A, 0x00, 0x00, 8)]
    [InlineData(CBOpcode.Res7_A, 0x80, 0x00, 8)]
    [InlineData(CBOpcode.Res7_A, 0x00, 0x00, 8)]
    public static void CBOperation_ResNA_ZeroFlagIsSetCorrectly(byte opcode, byte registerA, byte expectedRegisterA, byte expectedCycles)
    {
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor
                .Set8BitGeneralPurposeRegisters(registerA, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01)
                .SetProgramCounter(0x0001)
            )
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                [0x0001] = Opcode.Pre_CB,
                [0x0002] = opcode
            })
            .BuildGameBoy();
        
        var cycles = gameBoy.Update();
        var processor = gameBoy.GetProcessor();
        Assert.Equal(expectedCycles, cycles);
        Assert.Equal(expectedRegisterA, processor.GetValueOfRegisterA());
    }
}