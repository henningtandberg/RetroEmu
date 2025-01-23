using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests
{
    public class AddTests
    {
        [Theory]
        [InlineData(Opcode.Add_A_B, 4, 2)]
        [InlineData(Opcode.Add_A_C, 4, 2)]
        [InlineData(Opcode.Add_A_D, 4, 2)]
        [InlineData(Opcode.Add_A_E, 4, 2)]
        [InlineData(Opcode.Add_A_H, 4, 2)]
        [InlineData(Opcode.Add_A_L, 4, 2)]
        [InlineData(Opcode.Add_A_XHL, 8, 2)]
        [InlineData(Opcode.Add_A_A, 4, 2)]
        [InlineData(Opcode.Add_A_N8, 8, 2)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
                byte opcode, byte expectedCycles, byte expectedResult)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x01);
            memoryMock.Setup(mock => mock.Read(0x0101)).Returns(0x01);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.A = 0x01;
            processor.Registers.B = 0x01;
            processor.Registers.C = 0x01;
            processor.Registers.D = 0x01;
            processor.Registers.E = 0x01;
            processor.Registers.H = 0x01;
            processor.Registers.L = 0x01;
            processor.Registers.PC = 0x0001;
            
            var cycles = gameBoy.Update();
            
            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedResult, processor.Registers.A);
            Assert.False(processor.IsSet(Flag.Carry));
            Assert.False(processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
            Assert.False(processor.IsSet(Flag.Zero));
        }
        
        [Theory]
        [InlineData(Opcode.Add_A_B, 0)]
        [InlineData(Opcode.Add_A_C, 0)]
        [InlineData(Opcode.Add_A_D, 0)]
        [InlineData(Opcode.Add_A_E, 0)]
        [InlineData(Opcode.Add_A_H, 0)]
        [InlineData(Opcode.Add_A_L, 0)]
        [InlineData(Opcode.Add_A_XHL, 0)]
        [InlineData(Opcode.Add_A_A, 0)]
        [InlineData(Opcode.Add_A_N8, 0)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformed_ResultIsZeroAndZeroFlagIsSet(byte opcode, byte expectedResult)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x00);
            memoryMock.Setup(mock => mock.Read(0x0000)).Returns(0x00);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.A = 0x00;
            processor.Registers.B = 0x00;
            processor.Registers.C = 0x00;
            processor.Registers.D = 0x00;
            processor.Registers.E = 0x00;
            processor.Registers.H = 0x00;
            processor.Registers.L = 0x00;
            processor.Registers.PC = 0x0001;
            
            _ = gameBoy.Update();
            
            Assert.Equal(expectedResult, processor.Registers.A);
            Assert.True(processor.IsSet(Flag.Zero));
        }
        
        [Theory]
        [InlineData(Opcode.Add_A_B)]
        [InlineData(Opcode.Add_A_C)]
        [InlineData(Opcode.Add_A_D)]
        [InlineData(Opcode.Add_A_E)]
        [InlineData(Opcode.Add_A_H)]
        [InlineData(Opcode.Add_A_L)]
        [InlineData(Opcode.Add_A_XHL)]
        [InlineData(Opcode.Add_A_A)]
        [InlineData(Opcode.Add_A_N8)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithHalfCarry_HalfCarryFlagIsSet(byte opcode)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x08);
            memoryMock.Setup(mock => mock.Read(0x0808)).Returns(0x08);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.A = 0x08;
            processor.Registers.B = 0x08;
            processor.Registers.C = 0x08;
            processor.Registers.D = 0x08;
            processor.Registers.E = 0x08;
            processor.Registers.H = 0x08;
            processor.Registers.L = 0x08;
            processor.Registers.PC = 0x0001;
            
            _ = gameBoy.Update();
            
            Assert.True(processor.IsSet(Flag.HalfCarry));
        }
        
        [Theory]
        [InlineData(Opcode.Add_A_B)]
        [InlineData(Opcode.Add_A_C)]
        [InlineData(Opcode.Add_A_D)]
        [InlineData(Opcode.Add_A_E)]
        [InlineData(Opcode.Add_A_H)]
        [InlineData(Opcode.Add_A_L)]
        [InlineData(Opcode.Add_A_XHL)]
        [InlineData(Opcode.Add_A_A)]
        [InlineData(Opcode.Add_A_N8)]
        public static unsafe void
            WithAnyAddOpcode_AddInstructionIsPerformedWithCarry_CarryFlagIsSet(byte opcode)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x80);
            memoryMock.Setup(mock => mock.Read(0x8080)).Returns(0x80);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.A = 0x80;
            processor.Registers.B = 0x80;
            processor.Registers.C = 0x80;
            processor.Registers.D = 0x80;
            processor.Registers.E = 0x80;
            processor.Registers.H = 0x80;
            processor.Registers.L = 0x80;
            processor.Registers.PC = 0x0001;
            
            _ = gameBoy.Update();
            
            Assert.True(processor.IsSet(Flag.Carry));
        }

        [Theory]
        [InlineData(Opcode.Add_HL_BC, 8, 5, 7, 12, false, false)]
        [InlineData(Opcode.Add_HL_DE, 8, 5, 7, 12, false, false)]
        [InlineData(Opcode.Add_HL_HL, 8, 5, 5, 10, false, false)]
        [InlineData(Opcode.Add_HL_SP, 8, 5, 7, 12, false, false)]
        [InlineData(Opcode.Add_HL_BC, 8, 0x000F, 0x0001, 0x0010, false, false)]
        [InlineData(Opcode.Add_HL_BC, 8, 0x00FF, 0x0001, 0x0100, false, false)]
        [InlineData(Opcode.Add_HL_BC, 8, 0x0FFF, 0x0001, 0x1000, false, true)]
        [InlineData(Opcode.Add_HL_BC, 8, 0xFFFF, 0x0002, 0x0001, true, true)]
        public static unsafe void
            WithAnyAdd16Opcode_AddInstructionIsPerformedWithInputXYNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
                byte opcode, byte expectedCycles, ushort valueX, ushort valueY, ushort expectedResult, bool expectedCarry, bool expectedHalfCarry)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.BC = valueY;
            processor.Registers.DE = valueY;
            processor.Registers.HL = valueX;
            processor.Registers.SP = valueY;
            processor.Registers.PC = 0x0001;

            var cycles = gameBoy.Update();

            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedResult, processor.Registers.HL);
            Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
            Assert.Equal(expectedHalfCarry, processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
            Assert.False(processor.IsSet(Flag.Zero));
        }

        [Theory]
        [InlineData(Opcode.Add_SP_N8, 0x000F, 1, 0x0010, false, true)]
        [InlineData(Opcode.Add_SP_N8, 0x000F, -1, 0x000E, true, true)]
        [InlineData(Opcode.Add_SP_N8, 0x00FF, 1, 0x0100, true, true)]
        [InlineData(Opcode.Add_SP_N8, 0x00FF, -1, 0x00FE, true, true)]
        public static unsafe void
            WithSPAdd16Opcode_AddInstructionIsPerformedWithInputXYNoCarry_ResultIsAboveZeroAndNoFlagsAreSet(
                byte opcode, ushort valueX, sbyte valueY, ushort expectedResult, bool expectedCarry, bool expectedHalfCarry)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns((byte)valueY);
            var gameBoy = CreateGameBoy(memoryMock.Object);
            var processor = gameBoy.GetProcessor();
            processor.Registers.SP = valueX;
            processor.Registers.PC = 0x0001;

            var cycles = gameBoy.Update();

            Assert.Equal(16, cycles);
            Assert.Equal(expectedResult, processor.Registers.SP);
            Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
            Assert.Equal(expectedHalfCarry, processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
            Assert.False(processor.IsSet(Flag.Zero));
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