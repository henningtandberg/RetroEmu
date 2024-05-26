using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.CbTests;

public class CbSetTests
{
    [Theory]
    [InlineData(CBOpcode.Set0_A, 0x01, 0x01, 8)]
    [InlineData(CBOpcode.Set0_A, 0x00, 0x01, 8)]
    [InlineData(CBOpcode.Set1_A, 0x02, 0x02, 8)]
    [InlineData(CBOpcode.Set1_A, 0x00, 0x02, 8)]
    [InlineData(CBOpcode.Set2_A, 0x04, 0x04, 8)]
    [InlineData(CBOpcode.Set2_A, 0x00, 0x04, 8)]
    [InlineData(CBOpcode.Set3_A, 0x08, 0x08, 8)]
    [InlineData(CBOpcode.Set3_A, 0x00, 0x08, 8)]
    [InlineData(CBOpcode.Set4_A, 0x10, 0x10, 8)]
    [InlineData(CBOpcode.Set4_A, 0x00, 0x10, 8)]
    [InlineData(CBOpcode.Set5_A, 0x20, 0x20, 8)]
    [InlineData(CBOpcode.Set5_A, 0x00, 0x20, 8)]
    [InlineData(CBOpcode.Set6_A, 0x40, 0x40, 8)]
    [InlineData(CBOpcode.Set6_A, 0x00, 0x40, 8)]
    [InlineData(CBOpcode.Set7_A, 0x80, 0x80, 8)]
    [InlineData(CBOpcode.Set7_A, 0x00, 0x80, 8)]
    public static void CBOperation_SetNA_ZeroFlagIsSetCorrectly(byte opcode, byte registerA, byte expectedRegisterA, byte expectedCycles)
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