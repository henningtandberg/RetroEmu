using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using Xunit;

namespace RetroEmu.Devices.Tests
{
    public class AddInstructionTests
    {
        [Theory]
        [InlineData(0x80, 4, 2)]
        [InlineData(0x81, 4, 2)]
        [InlineData(0x82, 4, 2)]
        [InlineData(0x83, 4, 2)]
        [InlineData(0x84, 4, 2)]
        [InlineData(0x85, 4, 2)]
        [InlineData(0x86, 8, 3)]
        [InlineData(0x87, 4, 2)]
        [InlineData(0xC6, 8, 2)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithNoOverflow_ResultIsCorrectlyPlacedInRegisterA(byte opcode,
                byte expectedCycles, byte expectedResult)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Get(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Get(0x0002)).Returns(0x01);
            IGameBoy gameBoy = CreateGameBoy(memoryMock.Object);
            *gameBoy.GetProcessor().Registers.A = 0x01;
            *gameBoy.GetProcessor().Registers.B = 0x01;
            *gameBoy.GetProcessor().Registers.C = 0x01;
            *gameBoy.GetProcessor().Registers.D = 0x01;
            *gameBoy.GetProcessor().Registers.E = 0x01;
            *gameBoy.GetProcessor().Registers.H = 0x01;
            *gameBoy.GetProcessor().Registers.L = 0x01;
            *gameBoy.GetProcessor().Registers.PC = 0x0001;
            
            var cycles = gameBoy.Update();
            
            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedResult, *gameBoy.GetProcessor().Registers.A);
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