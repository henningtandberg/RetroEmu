using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.IsolatedOperationTests
{
    public class ALUOprationTestsBase
    {
        private enum ALUFetchType
        {
            B = 0,
            C,
            D,
            E,
            H,
            L,
            XHL,
            N8
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(fetch), fetch, null);
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

        [Theory]
        [InlineData(0x04, 0x03, false, 0x07, false, false, false, false)]
        [InlineData(0x08, 0x0f, false, 0x17, false, false, true, false)]
        [InlineData(0x08, 0xff, false, 0x07, false, false, true, true)]
        [InlineData(0xf0, 0xf0, false, 0xe0, false, false, false, true)]
        public static void Add_ResultCyclesAndFlagsAreSetAppropriately(
            byte a, byte b, bool carryIsSet, byte expectedResult, bool expectedZero, bool expectedSubtract,
            bool expectedHalfCarry, bool expectedCarry)
        {
            byte[] addOpcodes =
            [
                Opcode.Add_A_B,
                Opcode.Add_A_C,
                Opcode.Add_A_D,
                Opcode.Add_A_E,
                Opcode.Add_A_H,
                Opcode.Add_A_L,
                Opcode.Add_A_XHL,
                Opcode.Add_A_N8
            ];

            AssertThatAllOpcodesProduceExpectedResultCyclesAndFlags(a, b, carryIsSet, expectedResult, expectedZero, expectedSubtract, expectedHalfCarry, expectedCarry, addOpcodes);
        }

        private static void AssertThatAllOpcodesProduceExpectedResultCyclesAndFlags(byte a, byte b, bool carryIsSet,
            byte expectedResult, bool expectedZero, bool expectedSubtract, bool expectedHalfCarry, bool expectedCarry,
            IEnumerable<byte> addOpcodes)
        {
            addOpcodes
                .Zip(Enum.GetValues(typeof(ALUFetchType)).Cast<ALUFetchType>(), (opcode, fetch) => new {Opcode = opcode, Fetch = fetch})
                .ToList()
                .ForEach(operation =>
                {
                    TestALUOperation(operation.Opcode, operation.Fetch, a, b, carryIsSet, expectedResult, expectedZero, expectedSubtract, expectedHalfCarry, expectedCarry);
                });
        }

        static void TestALUOperation(byte opcode, ALUFetchType fetch, byte a, byte b, bool carry, byte expectedResult, bool expectedZero, bool expectedSubtract, bool expectedHalfCarry, bool expectedCarry)
        {
            var processorState = CreateProcessorState(fetch, a, b);
            var gameBoy = CreateTestGameBoy(opcode, carry, processorState);
            var expectedCycles = 4 + fetch switch
            {
                ALUFetchType.XHL => 4,
                ALUFetchType.N8 => 4,
                _ => 0
            };

            var processor = gameBoy.GetProcessor();
            if (carry) processor.SetFlag(Flag.Carry);

            var actualCycles = gameBoy.Update();
            var actualResult = processor.GetValueOfRegisterA();
            Assert.Equal(expectedCycles, actualCycles);
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedZero, processor.IsSet(Flag.Zero));
            Assert.Equal(expectedSubtract, processor.IsSet(Flag.Subtract));
            Assert.Equal(expectedHalfCarry, processor.IsSet(Flag.HalfCarry));
            Assert.Equal(expectedCarry, processor.IsSet(Flag.Carry));
        }

        private static IGameBoy CreateTestGameBoy(byte opcode, bool carry, ProcessorTestState processorState)
        {
            return TestGameBoyBuilder
                .CreateBuilder()
                .WithProcessor(processor => processor
                    .Set8BitGeneralPurposeRegisters(processorState.A, processorState.B, processorState.C, processorState.D, processorState.E, processorState.H, processorState.L)
                    .SetFlags(false, false, false, carry)
                    .SetProgramCounter(0x0000)
                )
                .WithMemory(() => new Dictionary<ushort, byte>
                {
                    [0x0000] = opcode,
                    [0x0001] = processorState.N8,
                    [0x01ff] = processorState.XHL,
                })
                .BuildGameBoy();
        }

        private record ProcessorTestState
        {
            public byte A { get; init; } = 0xff;
            public byte B { get; init; } = 0xff;
            public byte C { get; init; } = 0xff;
            public byte D { get; init; } = 0xff;
            public byte E { get; init; } = 0xff;
            public byte H { get; init; } = 0xff;
            public byte L { get; init; } = 0xff;
            public byte XHL { get; init; } = 0xff; // Value at address HL
            public byte N8 { get; init; } = 0xff; // 8-bit immediate value
        }

        private static ProcessorTestState CreateProcessorState(ALUFetchType fetch, byte a, byte b)
        {
            return fetch switch
            {
                ALUFetchType.B => new ProcessorTestState { A = a, B = b },
                ALUFetchType.C => new ProcessorTestState { A = a, C = b },
                ALUFetchType.D => new ProcessorTestState { A = a, D = b },
                ALUFetchType.E => new ProcessorTestState { A = a, E = b },
                ALUFetchType.H => new ProcessorTestState { A = a, H = b },
                ALUFetchType.L => new ProcessorTestState { A = a, L = b },
                ALUFetchType.XHL => new ProcessorTestState { A = a, XHL = b, H = 0x01, L = 0xff },
                ALUFetchType.N8 => new ProcessorTestState { A = a, N8 = b },
                _ => throw new ArgumentOutOfRangeException(nameof(fetch), fetch, null)
            };
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