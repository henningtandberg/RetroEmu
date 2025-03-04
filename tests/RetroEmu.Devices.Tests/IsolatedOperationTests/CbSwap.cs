using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests
{
    public class CbSwapTests
    {
        [Theory]
        [InlineData(0b11110000, 0b00001111, false)]
        [InlineData(0b10101010, 0b10101010, false)]
        [InlineData(0b11000011, 0b00111100, false)]
        [InlineData(0b11111111, 0b11111111, false)]
        [InlineData(0b00000000, 0b00000000, true)]
        public static void CBOperation_SwapA_ValueIsSwappedAndZeroFlagIsSetCorrectly(byte registerA, byte expectedResult, bool expectedZeroFlag)
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
                    [0x0002] = CBOpcode.Swap_A,
                })
                .BuildGameBoy();
            
            var cycles = gameBoy.Update();
            var processor = (ITestableProcessor)gameBoy.GetProcessor();
            Assert.Equal(8, cycles);
            Assert.Equal(expectedResult, processor.GetValueOfRegisterA());
            Assert.Equal(expectedZeroFlag, processor.ZeroFlagIsSet());
            Assert.False(processor.CarryFlagIsSet());
            Assert.False(processor.HalfCarryFlagIsSet());
            Assert.False(processor.SubtractFlagIsSet());
        }
    }
}