using RetroEmu.Devices.DMG.CPU;
using System.Collections.Generic;
using Xunit;

namespace RetroEmu.Devices.Tests
{
    public class MiniProgramTests
    {
        [Theory]
        [InlineData(1, 1, 1, 2)]
        [InlineData(1, 1, 2, 3)]
        [InlineData(1, 1, 4, 8)]
        [InlineData(1, 1, 5, 13)]
        [InlineData(1, 1, 6, 21)]
        [InlineData(1, 1, 7, 34)]
        [InlineData(1, 1, 8, 55)]
        [InlineData(1, 1, 9, 89)]
        [InlineData(1, 1, 10, 144)]
        [InlineData(1, 1, 11, 233)]
        public static unsafe void
            WithFibonacciProgram_RunProgram_ResultIsExpected(byte a, byte b, byte iterations, byte expectedValue)
        {
            var gameBoy = TestGameBoyBuilder
               .CreateBuilder()
               .WithProcessor(processor => processor
                   .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                   .SetProgramCounter(0x0001)
            )
               // Pseudocode:
               // B = a;
               // C = b;
               // L = iterations;
               // do
               // {
               //   A = B
               //   A = A + C;
               //   B = C;
               //   C = A;
               //   L--;
               // } while (L > 0)
               .WithMemory(() => new Dictionary<ushort, byte>
               {
                   [0x0001] = Opcode.Ld_B_N8,
                   [0x0002] = a,
                   [0x0003] = Opcode.Ld_C_N8,
                   [0x0004] = b,
                   [0x0005] = Opcode.Ld_H_N8,
                   [0x0006] = 0,
                   [0x0007] = Opcode.Ld_L_N8,
                   [0x0008] = iterations,
                   [0x0009] = Opcode.Ld_A_B,
                   [0x000A] = Opcode.Add_A_C,
                   [0x000B] = Opcode.Ld_B_C,
                   [0x000C] = Opcode.Ld_C_A,
                   [0x000D] = Opcode.Dec_L,
                   [0x000E] = Opcode.JpNZ_N16,
                   [0x000F] = 0x09,
                   [0x0010] = 0x00, // 0x0009
                   [0x0011] = Opcode.Ld_A_C
               })
               .BuildGameBoy();

            var instruction_count = 4 + 6 * iterations + 1;
            for (var i = 0; i < instruction_count; i++)
            {
                var cycles = gameBoy.Update();
            }
            var processor = gameBoy.GetProcessor();
            Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
        }

        [Theory (Skip = "Not sure if this is correct as RLCA sets the carry flag to old bit 7")]
        [InlineData(0x01, 1)]
        [InlineData(0x02, 1)]
        [InlineData(0x0f, 4)]
        [InlineData(0xf1, 5)]
        [InlineData(0xff, 8)]
        public static unsafe void
            WithBitCounterProgram_RunProgram_ResultIsExpected(byte a, byte expectedValue)
        {
            var gameBoy = TestGameBoyBuilder
               .CreateBuilder()
               .WithProcessor(processor => processor
                   .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                   .SetProgramCounter(0x0001)
            )
               // Pseudocode:
               // B = a; <- Input
               // C = 1; <- Current bit we are looking at
               // D = 0; <- Bit count
               // do
               // {
               //   A = B;
               //   A = A & C;
               //   A = A + 0xff; <- Will overflow if A != 0;
               //   A = 0;
               //   if (CarryFlag is set) A = 1;
               //   A = A + D;
               //   D = A;
               //   A = C;
               //   A << 1;
               //   C = A;
               // } while (CarryFlag not set)
               .WithMemory(() => new Dictionary<ushort, byte>
               {
                   [0x0001] = Opcode.Ld_B_N8,
                   [0x0002] = a,
                   [0x0003] = Opcode.Ld_C_N8,
                   [0x0004] = 1,
                   [0x0005] = Opcode.Ld_D_N8,
                   [0x0006] = 0,
                   [0x0007] = Opcode.Ld_A_B,
                   [0x0008] = Opcode.And_A_C,
                   [0x0009] = Opcode.Add_A_N8,
                   [0x000A] = 0xff,
                   [0x000B] = Opcode.Ld_A_N8,
                   [0x000C] = 0x00,
                   [0x000D] = Opcode.Adc_A_N8,
                   [0x000E] = 0x00,
                   [0x000F] = Opcode.Add_A_D,
                   [0x0010] = Opcode.Ld_D_A,
                   [0x0011] = Opcode.Ld_A_C,
                   [0x0012] = Opcode.Rlc_A,
                   [0x0013] = Opcode.Ld_C_A,
                   [0x0014] = Opcode.JpNC_N16,
                   [0x0015] = 0x07,
                   [0x0016] = 0x00,
                   [0x0017] = Opcode.Ld_A_D,
                   [0x0018] = Opcode.Nop
               })
               .BuildGameBoy();

            var processor = gameBoy.GetProcessor();
            while (processor.GetValueOfRegisterPC() != 0x18)
            {
                _ = gameBoy.Update();
            }
            Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
        }
    
        [Theory]
        [InlineData(0x01, 1)]
        [InlineData(0x02, 1)]
        [InlineData(0x0f, 4)]
        [InlineData(0xf1, 5)]
        [InlineData(0xff, 8)]
        public static unsafe void
            GivenBitCountingProgram_RunProgram_ResultIsExpected(byte a, byte expectedValue)
        {
            var gameBoy = TestGameBoyBuilder
               .CreateBuilder()
               .WithProcessor(processor => processor
                   .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
                   .SetProgramCounter(0x0001))
               .WithMemory(() => new Dictionary<ushort, byte>
               {
                   [0x0001] = Opcode.Ld_A_N8,   // A = a; <- Input
                   [0x0002] = a,
                   [0x0003] = Opcode.Ld_B_N8,   // B = 8; <- Bits left to count
                   [0x0004] = 0x08,
                   [0x0005] = Opcode.Ld_C_N8,   // C = 0; <- Bit count
                   [0x0006] = 0,
                   [0x0007] = Opcode.Rlc_A,     // A << 1; <- Old bit 7 is now in carry flag
                   [0x0008] = Opcode.JpNC_N16,  // Jump to 0x0012 if carry flag is not set
                   [0x0009] = 0x0C,
                   [0x000A] = 0x00,
                   [0x000B] = Opcode.Inc_C,     // Increment C (Counter)
                   [0x000C] = Opcode.Dec_B,     // Decrement B (Bits left to count)
                   [0x000D] = Opcode.JpNZ_N16,  // Jump to 0x0007
                   [0x000E] = 0x07,
                   [0x000F] = 0x00,
                   [0x0010] = Opcode.Ld_A_C     // Load count into A
               })
               .BuildGameBoy();

            var processor = gameBoy.GetProcessor();
            while (processor.GetValueOfRegisterPC() != 0x11)
            {
                _ = gameBoy.Update();
            }
            Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
        }
    }
}