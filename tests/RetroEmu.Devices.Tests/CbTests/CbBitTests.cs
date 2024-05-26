using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.CbTests
{
    public class CbBitTests
    {
        [Theory]
        [InlineData(CBOpcode.Bit0_A, 0x01, 8, false)]
        [InlineData(CBOpcode.Bit0_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit1_A, 0x02, 8, false)]
        [InlineData(CBOpcode.Bit1_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit2_A, 0x04, 8, false)]
        [InlineData(CBOpcode.Bit2_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit3_A, 0x08, 8, false)]
        [InlineData(CBOpcode.Bit3_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit4_A, 0x10, 8, false)]
        [InlineData(CBOpcode.Bit4_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit5_A, 0x20, 8, false)]
        [InlineData(CBOpcode.Bit5_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit6_A, 0x40, 8, false)]
        [InlineData(CBOpcode.Bit6_A, 0x00, 8, true)]
        [InlineData(CBOpcode.Bit7_A, 0x80, 8, false)]
        [InlineData(CBOpcode.Bit7_A, 0x00, 8, true)]
        public static void CBOperation_BitNA_ZeroFlagIsSetCorrectly(byte opcode, byte registerA, byte expectedCycles, bool expectedZeroFlag)
        {
            var gameBoy = TestGameBoyBuilder
                .CreateBuilder()
                .WithProcessor(processor => processor
                    .Set8BitGeneralPurposeRegisters(registerA, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01)
                    .SetFlags(false, true, false, false)
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
            Assert.Equal(expectedZeroFlag, processor.IsSet(Flag.Zero));
            Assert.False(processor.IsSet(Flag.Carry));
            Assert.True(processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
        }
    }
}