using RetroEmu.Devices.DMG.CPU;
using Xunit;

namespace RetroEmu.GB.Tests.IsolatedOperationTests;

public class ALUOprationTestsBase
{
    [Theory (Skip = "Use this for creating complete test coverage of operations")]
    [InlineData(Opcode.Sbc_A_B, 0x04, 0x03, false, 0x01, false, true, false, false)]
    [InlineData(Opcode.Sbc_A_B, 0x04, 0x03, true, 0x00, true, true, false, false)]
    [InlineData(Opcode.Sub_A_B, 0x04, 0x03, false, 0x01, false, true, false, false)]
    [InlineData(Opcode.Sub_A_B, 0x04, 0x03, true, 0x01, false, true, false, false)]
    [InlineData(Opcode.Sub_A_B, 0x04, 0x04, false, 0x00, true, true, false, false)]
    [InlineData(Opcode.Sub_A_B, 0x00, 0x01, false, 0xff, false, true, true, true)]
    [InlineData(Opcode.Sub_A_B, 0x10, 0x01, false, 0x0f, false, true, true, false)]
    [InlineData(Opcode.Or_A_B, 0xf0, 0x0f, false, 0xff, false, false, false, false)]
    [InlineData(Opcode.Or_A_B, 0x00, 0x00, false, 0x00, true, false, false, false)]
    [InlineData(Opcode.Or_A_B, 0x11, 0x30, false, 0x31, false, false, false, false)]
    [InlineData(Opcode.Xor_A_B, 0xf0, 0xf0, false, 0x00, true, false, false, false)]
    [InlineData(Opcode.Xor_A_B, 0xf0, 0x0f, false, 0xff, false, false, false, false)]
    [InlineData(Opcode.Xor_A_B, 0xf0, 0xff, false, 0x0f, false, false, false, false)]
    [InlineData(Opcode.Xor_A_B, 0x01, 0x01, false, 0x00, true, false, false, false)]
    [InlineData(Opcode.Cp_A_B, 0x04, 0x03, false, 0x04, false, true, false, false)]
    [InlineData(Opcode.Cp_A_B, 0x04, 0x03, true, 0x04, false, true, false, false)]
    [InlineData(Opcode.Cp_A_B, 0x04, 0x04, false, 0x04, true, true, false, false)]
    [InlineData(Opcode.Cp_A_B, 0x00, 0x01, false, 0x00, false, true, true, true)]
    [InlineData(Opcode.Cp_A_B, 0x10, 0x01, false, 0x10, false, true, true, false)]
    public static void WithAnyALUOpcode_InstructionIsPerformed_ResultIsExpected(
        byte opcode, byte a, byte b, bool carry, byte expectedResult, bool expectedZero, bool expectedSubtract, bool expectedHalfCarry, bool expectedCarry)
    { }
}