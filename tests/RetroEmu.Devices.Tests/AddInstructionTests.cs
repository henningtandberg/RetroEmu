using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
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
        [InlineData(0x86, 8, 2)]
        [InlineData(0x87, 4, 2)]
        [InlineData(0xC6, 8, 2)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
                byte opcode, byte expectedCycles, byte expectedResult)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Get(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Get(0x0002)).Returns(0x01);
            memoryMock.Setup(mock => mock.Get(0x0101)).Returns(0x01);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            *processor.Registers.A = 0x01;
            *processor.Registers.B = 0x01;
            *processor.Registers.C = 0x01;
            *processor.Registers.D = 0x01;
            *processor.Registers.E = 0x01;
            *processor.Registers.H = 0x01;
            *processor.Registers.L = 0x01;
            *processor.Registers.PC = 0x0001;
            
            var cycles = gameBoy.Update();
            
            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedResult, *processor.Registers.A);
            Assert.False(processor.IsSet(Flag.Carry));
            Assert.False(processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
            Assert.False(processor.IsSet(Flag.Zero));
        }
        
        [Theory]
        [InlineData(0x80, 0)]
        [InlineData(0x81, 0)]
        [InlineData(0x82, 0)]
        [InlineData(0x83, 0)]
        [InlineData(0x84, 0)]
        [InlineData(0x85, 0)]
        [InlineData(0x86, 0)]
        [InlineData(0x87, 0)]
        [InlineData(0xC6, 0)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformed_ResultIsZeroAndZeroFlagIsSet(byte opcode, byte expectedResult)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Get(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Get(0x0002)).Returns(0x00);
            memoryMock.Setup(mock => mock.Get(0x0000)).Returns(0x00);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            *processor.Registers.A = 0x00;
            *processor.Registers.B = 0x00;
            *processor.Registers.C = 0x00;
            *processor.Registers.D = 0x00;
            *processor.Registers.E = 0x00;
            *processor.Registers.H = 0x00;
            *processor.Registers.L = 0x00;
            *processor.Registers.PC = 0x0001;
            
            _ = gameBoy.Update();
            
            Assert.Equal(expectedResult, *processor.Registers.A);
            Assert.True(processor.IsSet(Flag.Zero));
        }
        
        [Theory]
        [InlineData(0x80)]
        [InlineData(0x81)]
        [InlineData(0x82)]
        [InlineData(0x83)]
        [InlineData(0x84)]
        [InlineData(0x85)]
        [InlineData(0x86)]
        [InlineData(0x87)]
        [InlineData(0xC6)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithHalfCarry_HalfCarryFlagIsSet(byte opcode)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Get(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Get(0x0002)).Returns(0x08);
            memoryMock.Setup(mock => mock.Get(0x0808)).Returns(0x08);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            *processor.Registers.A = 0x08;
            *processor.Registers.B = 0x08;
            *processor.Registers.C = 0x08;
            *processor.Registers.D = 0x08;
            *processor.Registers.E = 0x08;
            *processor.Registers.H = 0x08;
            *processor.Registers.L = 0x08;
            *processor.Registers.PC = 0x0001;
            
            _ = gameBoy.Update();
            
            Assert.True(processor.IsSet(Flag.HalfCarry));
        }
        
        [Theory]
        [InlineData(0x80)]
        [InlineData(0x81)]
        [InlineData(0x82)]
        [InlineData(0x83)]
        [InlineData(0x84)]
        [InlineData(0x85)]
        [InlineData(0x86)]
        [InlineData(0x87)]
        [InlineData(0xC6)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithCarry_CarryFlagIsSet(byte opcode)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Get(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Get(0x0002)).Returns(0x80);
            memoryMock.Setup(mock => mock.Get(0x8080)).Returns(0x80);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            *processor.Registers.A = 0x80;
            *processor.Registers.B = 0x80;
            *processor.Registers.C = 0x80;
            *processor.Registers.D = 0x80;
            *processor.Registers.E = 0x80;
            *processor.Registers.H = 0x80;
            *processor.Registers.L = 0x80;
            *processor.Registers.PC = 0x0001;
            
            _ = gameBoy.Update();
            
            Assert.True(processor.IsSet(Flag.Carry));
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