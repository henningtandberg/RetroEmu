using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests
{
    public class RotateTests
    {
        [Theory]
        [InlineData(Opcode.Rlc_A, 0b11110000, false, 0b11100001, true, false)]
        [InlineData(Opcode.Rlc_A, 0b00000000, true, 0b00000000, false, true)]
        [InlineData(Opcode.Rla, 0b11110000, false, 0b11100000, true, false)]
        [InlineData(Opcode.Rla, 0b00000000, true, 0b00000001, false, false)]
        [InlineData(Opcode.Rrc_A, 0b00001111, false, 0b10000111, true, false)]
        [InlineData(Opcode.Rrc_A, 0b00000000, true, 0b00000000, false, true)]
        [InlineData(Opcode.Rra, 0b00001111, false, 0b00000111, true, false)]
        [InlineData(Opcode.Rra, 0b00000000, true, 0b10000000, false, false)]
        public static unsafe void
            WithAnyRotateOpcode_RotateAWithInputValue_ResultCarryAndZeroIsExpected(
                byte opcode, byte input, bool inputCarry, byte expectedResult, bool expectedCarry, bool expectedZero)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.A = input;
            processor.Registers.PC = 0x0001;

            if (inputCarry)
            {
                processor.SetFlag(Flag.Carry);
            }

            var cycles = gameBoy.Update();

            Assert.Equal(4, cycles);
            Assert.Equal(expectedResult, processor.Registers.A);
            Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
            Assert.False(processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
            Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
        }

        private static IGameBoy CreateGameBoy(IMemory memoryMockObject)
        {
            return new ServiceCollection()
                .AddDotMatrixGameBoy()
                .AddSingleton(memoryMockObject)
                .BuildServiceProvider()
                .GetRequiredService<IGameBoy>();
        }
    }
}