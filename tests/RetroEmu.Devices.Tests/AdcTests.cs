using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using Xunit;

namespace RetroEmu.Devices.Tests
{
    public class AdcTests
    {
        [Theory]
        [InlineData(0x88, 1, 1, 4, 2)] // Adc B
        [InlineData(0x89, 1, 1, 4, 2)] // Adc C
        [InlineData(0x8A, 1, 1, 4, 2)] // Adc D
        [InlineData(0x8B, 1, 1, 4, 2)] // Adc E
        [InlineData(0x8C, 1, 1, 4, 2)] // Adc H
        [InlineData(0x8D, 1, 1, 4, 2)] // Adc L
        [InlineData(0x8E, 1, 1, 8, 2)] // Adc (HL)
        [InlineData(0x8F, 1, 1, 4, 2)] // Adc A
        [InlineData(0xCE, 1, 1, 8, 2)] // Adc n
        public static void GivenAdc_WhenInstructionIsPerformedWithInputXYCausingNoOverflow_ThenCyclesAndResultAreCorrectWithNoCarryFlag(
            byte opcode, byte valueX, byte valueY, byte expectedCycles, byte expectedSum)
        {
            var gameBoy = TestGameBoyBuilder
                .CreateBuilder()
                .WithProcessor(processor => processor
                    .Set8BitGeneralPurposeRegisters(0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01)
                    .SetProgramCounter(0x0001)
                )
                .WithMemory(() => new Dictionary<ushort, byte>
                {
                    [0x0001] = opcode,
                    [0x0002] = valueX,
                    [0x0101] = valueY
                })
                .BuildGameBoy();
            
            var cycles = gameBoy.Update();
            var processor = gameBoy.GetProcessor();
            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedSum, processor.GetValueOfRegisterA());
            Assert.False(processor.IsSet(Flag.Carry));
            Assert.False(processor.IsSet(Flag.HalfCarry));
            Assert.False(processor.IsSet(Flag.Subtract));
            Assert.False(processor.IsSet(Flag.Zero));
        }

        [Theory]
        [InlineData(0x88, 1, 0xFF, 4, 0)] // Adc B
        [InlineData(0x89, 1, 0xFF, 4, 0)] // Adc C
        [InlineData(0x8A, 1, 0xFF, 4, 0)] // Adc D
        [InlineData(0x8B, 1, 0xFF, 4, 0)] // Adc E
        [InlineData(0x8C, 1, 0xFF, 4, 0)] // Adc H
        [InlineData(0x8D, 1, 0xFF, 4, 0)] // Adc L
        [InlineData(0x8E, 1, 0xFF, 8, 0)] // Adc (HL)
        [InlineData(0x8F, 0xFF, 0xFF, 4, 0xFE)] // Adc A
        [InlineData(0xCE, 1, 0xFF, 8, 0)] // Adc n
        public static void GivenAdc_WhenInstructionIsPerformedWithInputXYCausingOverflow_ThenCyclesAndResultAreCorrectWithCarryFlagSet(
            byte opcode, byte valueX, byte valueY, byte expectedCycles, byte expectedSum)
        {
            var gameBoy = TestGameBoyBuilder
                .CreateBuilder()
                .WithProcessor(processor => processor
                    .Set8BitGeneralPurposeRegisters(valueX, valueY, valueY, valueY, valueY, valueY, valueY)
                    .SetProgramCounter(0x0001)
                )
                .WithMemory(() => new Dictionary<ushort, byte>
                {
                    [0x0001] = opcode,
                    [0x0002] = valueY,
                    [0xFFFF] = valueY
                })
                .BuildGameBoy();
            
            var cycles = gameBoy.Update();
            var processor = gameBoy.GetProcessor();
            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedSum, processor.GetValueOfRegisterA());
            Assert.True(processor.IsSet(Flag.Carry));
        }

        [Theory]
        [InlineData(0x88, 0)] // Adc B
        [InlineData(0x89, 0)] // Adc C
        [InlineData(0x8A, 0)] // Adc D
        [InlineData(0x8B, 0)] // Adc E
        [InlineData(0x8C, 0)] // Adc H
        [InlineData(0x8D, 0)] // Adc L
        [InlineData(0x8E, 0)] // Adc (HL)
        [InlineData(0x8F, 0)] // Adc A
        [InlineData(0xCE, 0)] // Adc n
        public static unsafe void
            WithAnyAdcOpcode_AdcInstructionIsPerformed_ResultIsZeroAndZeroFlagIsSet(byte opcode, byte expectedResult)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x00);
            memoryMock.Setup(mock => mock.Read(0x0000)).Returns(0x00);
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
        [InlineData(0x88)] // Adc B
        [InlineData(0x89)] // Adc C
        [InlineData(0x8A)] // Adc D
        [InlineData(0x8B)] // Adc E
        [InlineData(0x8C)] // Adc H
        [InlineData(0x8D)] // Adc L
        [InlineData(0x8E)] // Adc (HL)
        [InlineData(0x8F)] // Adc A
        [InlineData(0xCE)] // Adc n
        public static unsafe void
            WithAnyAdcOpcode_AdcInstructionIsPerformedWithHalfCarry_HalfCarryFlagIsSet(byte opcode)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x08);
            memoryMock.Setup(mock => mock.Read(0x0808)).Returns(0x08);
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
        [InlineData(0x88)]
        [InlineData(0x89)]
        [InlineData(0x8A)]
        [InlineData(0x8B)]
        [InlineData(0x8C)]
        [InlineData(0x8D)]
        [InlineData(0x8E)]
        [InlineData(0x8F)]
        [InlineData(0xCE)]
        public static unsafe void
            WithAnyAdcOpcode_AdcInstructionIsPerformedWithCarry_CarryFlagIsSet(byte opcode)
        {
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(mock => mock.Read(0x0001)).Returns(opcode);
            memoryMock.Setup(mock => mock.Read(0x0002)).Returns(0x80);
            memoryMock.Setup(mock => mock.Read(0x8080)).Returns(0x80);
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