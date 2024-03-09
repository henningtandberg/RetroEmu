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
                   [0x0001] = OPC.Ld_B_N8,
                   [0x0002] = a,
                   [0x0003] = OPC.Ld_C_N8,
                   [0x0004] = b,
                   [0x0005] = OPC.Ld_H_N8,
                   [0x0006] = 0,
                   [0x0007] = OPC.Ld_L_N8,
                   [0x0008] = iterations,
                   [0x0009] = OPC.Ld_A_B,
                   [0x000A] = OPC.Add_A_C,
                   [0x000B] = OPC.Ld_B_C,
                   [0x000C] = OPC.Ld_C_A,
                   [0x000D] = OPC.Dec_L,
                   [0x000E] = OPC.JpNZ_N16,
                   [0x000F] = 0x09,
                   [0x0010] = 0x00, // 0x0009
                   [0x0011] = OPC.Ld_A_C
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
    }
}