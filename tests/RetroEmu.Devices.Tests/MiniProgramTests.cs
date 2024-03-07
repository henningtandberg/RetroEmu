using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json.Linq;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Xunit;

namespace RetroEmu.Devices.Tests
{
    public class MiniProgramTests
    {
        [Theory(Skip = "Not yet implemented")]
        [InlineData(1, 1, 1, 2)]
        [InlineData(1, 1, 2, 3)]
        [InlineData(1, 1, 3, 5)]
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
                   [0x0001] = 0x06, // LD B, n
                   [0x0002] = a,
                   [0x0003] = 0x0E, // LD C, n
                   [0x0004] = b,
                   [0x0005] = 0x26, // LD H, n
                   [0x0006] = 0,
                   [0x0007] = 0x2E, // LD L, n
                   [0x0008] = iterations,
                   [0x0009] = 0x78, // LD A, B
                   [0x000A] = 0x81, // Add A, C
                   [0x000B] = 0x41, // LD B, C
                   [0x000C] = 0x4F, // LD C, A
                   [0x000D] = 0x2D, // DEC L (Missing atm)
                   [0x000E] = 0xC2, // JP NZ 0x0009 (Missing atm)
                   [0x000F] = 0x09, // 0x09
                   [0x0010] = 0x00, // 0x00
                   [0x0011] = 0x79 // LD A, C
               })
               .BuildGameBoy();

            var cycles = gameBoy.Update();
            var processor = gameBoy.GetProcessor();
            Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
        }
    }
}