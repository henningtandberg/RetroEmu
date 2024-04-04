using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.AluTests
{
    public class AdcTests
    {
        [Theory]
        [InlineData(Opcode.Adc_A_B, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_C, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_D, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_E, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_H, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_L, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_XHL, 1, 1, 8, 2)]
        [InlineData(Opcode.Adc_A_A, 1, 1, 4, 2)]
        [InlineData(Opcode.Adc_A_N8, 1, 1, 8, 2)]
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
        [InlineData(Opcode.Adc_A_B, 1, 0xFF, 4, 0)]
        [InlineData(Opcode.Adc_A_C, 1, 0xFF, 4, 0)]
        [InlineData(Opcode.Adc_A_D, 1, 0xFF, 4, 0)]
        [InlineData(Opcode.Adc_A_E, 1, 0xFF, 4, 0)]
        [InlineData(Opcode.Adc_A_H, 1, 0xFF, 4, 0)]
        [InlineData(Opcode.Adc_A_L, 1, 0xFF, 4, 0)]
        [InlineData(Opcode.Adc_A_XHL, 1, 0xFF, 8, 0)]
        [InlineData(Opcode.Adc_A_A, 0xFF, 0xFF, 4, 0xFE)]
        [InlineData(Opcode.Adc_A_N8, 1, 0xFF, 8, 0)]
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
        [InlineData(Opcode.Adc_A_B, 0)]
        [InlineData(Opcode.Adc_A_C, 0)]
        [InlineData(Opcode.Adc_A_D, 0)]
        [InlineData(Opcode.Adc_A_E, 0)]
        [InlineData(Opcode.Adc_A_H, 0)]
        [InlineData(Opcode.Adc_A_L, 0)]
        [InlineData(Opcode.Adc_A_XHL, 0)]
        [InlineData(Opcode.Adc_A_A, 0)]
        [InlineData(Opcode.Adc_A_N8, 0)]
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
        [InlineData(Opcode.Adc_A_B)]
        [InlineData(Opcode.Adc_A_C)]
        [InlineData(Opcode.Adc_A_D)]
        [InlineData(Opcode.Adc_A_E)]
        [InlineData(Opcode.Adc_A_H)]
        [InlineData(Opcode.Adc_A_L)]
        [InlineData(Opcode.Adc_A_XHL)]
        [InlineData(Opcode.Adc_A_A)]
        [InlineData(Opcode.Adc_A_N8)]
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
        [InlineData(Opcode.Adc_A_B)]
        [InlineData(Opcode.Adc_A_C)]
        [InlineData(Opcode.Adc_A_D)]
        [InlineData(Opcode.Adc_A_E)]
        [InlineData(Opcode.Adc_A_H)]
        [InlineData(Opcode.Adc_A_L)]
        [InlineData(Opcode.Adc_A_XHL)]
        [InlineData(Opcode.Adc_A_A)]
        [InlineData(Opcode.Adc_A_N8)]
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