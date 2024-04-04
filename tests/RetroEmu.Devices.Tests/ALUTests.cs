using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json.Linq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace RetroEmu.Devices.Tests
{
    public class ALUTests
    {
        private enum ALUFetchType
        {
            B = 0, C, D, E, H, L, XHL
        }

        static void TestALU(byte opcode, ALUFetchType fetch, byte a, byte b, bool carry, byte expectedResult, bool expectedZero, bool expectedSubtract, bool expectedHalfCarry, bool expectedCarry)
        {
            Assert.Equal(0, opcode & 0x7);

            byte regA = a;
            byte regB = 0xff;
            byte regC = 0xff;
            byte regD = 0xff;
            byte regE = 0xff;
            byte regH = 0xff;
            byte regL = 0xff;
            byte XHL = 0xff;

            switch (fetch)
            {
                case ALUFetchType.B:
                    regB = b;
                    break;
                case ALUFetchType.C:
                    regC = b;
                    break;
                case ALUFetchType.D:
                    regD = b;
                    break;
                case ALUFetchType.E:
                    regE = b;
                    break;
                case ALUFetchType.H:
                    regH = b;
                    break;
                case ALUFetchType.L:
                    regL = b;
                    break;
                case ALUFetchType.XHL:
                    XHL = b;
                    regH = 0x01;
                    regL = 0xff;
                    break;
            }

            byte properOpcode = (byte)(opcode + (byte)fetch);

            var gameBoy = TestGameBoyBuilder
               .CreateBuilder()
               .WithProcessor(processor => processor
                   .Set8BitGeneralPurposeRegisters(regA, regB, regC, regD, regE, regH, regL)
                   .SetProgramCounter(0x0000)
               )
               .WithMemory(() => new Dictionary<ushort, byte>
               {
                   [0x0000] = properOpcode,
                   [0x01ff] = XHL,
               })
               .BuildGameBoy();

            var processor = gameBoy.GetProcessor();
            if (carry) processor.SetFlag(Flag.Carry);

            var cycles = gameBoy.Update();
            var result = processor.GetValueOfRegisterA();
            
            var expectedCycles = 4 + (fetch == ALUFetchType.XHL ? 4 : 0);

            Assert.Equal(expectedCycles, cycles);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
            Assert.Equal(expectedSubtract, processor.IsSet(Flag.Subtract));
            Assert.Equal(expectedHalfCarry, processor.IsSet(Flag.HalfCarry));
            Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));

        }


        [Theory]
        [InlineData(Opcode.Adc_A_B, 0x04, 0x03, false, 0x07, false, false, false, false)]
        [InlineData(Opcode.Adc_A_B, 0x04, 0x03, true, 0x08, false, false, false, false)]
        [InlineData(Opcode.Add_A_B, 0x04, 0x03, false, 0x07, false, false, false, false)]
        [InlineData(Opcode.And_A_B, 0x08, 0xff, false, 0x08, false, false, true, false)]
        [InlineData(Opcode.And_A_B, 0xf0, 0x0f, false, 0x00, true, false, true, false)]
        [InlineData(Opcode.Sbc_A_B, 0x04, 0x03, false, 0x01, false, true, false, false)]
        [InlineData(Opcode.Sbc_A_B, 0x04, 0x03, true, 0x00, true, true, false, false)]
        [InlineData(Opcode.Sub_A_B, 0x04, 0x03, false, 0x01, false, true, false, false)]
        [InlineData(Opcode.Sub_A_B, 0x04, 0x03, true, 0x01, false, true, false, false)]
        [InlineData(Opcode.Sub_A_B, 0x04, 0x04, false, 0x00, true, true, false, false)]
        [InlineData(Opcode.Sub_A_B, 0x00, 0x01, false, 0xff, false, true, true, true)]
        [InlineData(Opcode.Sub_A_B, 0x10, 0x01, false, 0x0f, false, true, true, false)]
        //[InlineData(Opcode.Or_A_B, 0xf0, 0x0f, false, 0xff, false, false, false, false)]
        //[InlineData(Opcode.Or_A_B, 0x00, 0x00, false, 0x00, true, false, false, false)]
        //[InlineData(Opcode.Or_A_B, 0x11, 0x30, false, 0x31, false, false, false, false)]
        //[InlineData(Opcode.Xor_A_B, 0xf0, 0xf0, false, 0xff, false, false, false, false)]
        //[InlineData(Opcode.Xor_A_B, 0x01, 0x01, false, 0x00, true, false, false, false)]
        //[InlineData(Opcode.Cp_A_B, 0x04, 0x03, false, 0x04, false, true, false, false)]
        //[InlineData(Opcode.Cp_A_B, 0x04, 0x03, true, 0x04, false, true, false, false)]
        //[InlineData(Opcode.Cp_A_B, 0x04, 0x04, false, 0x04, true, true, false, false)]
        //[InlineData(Opcode.Cp_A_B, 0x00, 0x01, false, 0x04, false, true, true, true)]
        //[InlineData(Opcode.Cp_A_B, 0x10, 0x01, false, 0x10, false, true, true, false)]
        public static unsafe void
            WithAnyALUOpcode_InstructionIsPerformed_ResultIsExpected(
                byte opcode, byte a, byte b, bool carry, byte expectedResult, bool expectedZero, bool expectedSubtract, bool expectedHalfCarry, bool expectedCarry)
        {
           for (var i =0; i < 7; i++) 
           {
                var fetch = (ALUFetchType)i;
                TestALU(opcode, fetch, a, b, carry, expectedResult, expectedZero, expectedSubtract, expectedHalfCarry, expectedCarry);
           }
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