﻿using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;

namespace RetroEmu.GB.Tests.MiniProgramTests;
public class FibonacciProgramTest
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
    public static void
        FibonacciProgram_RunSpecificAmountOfCycles_ResultIsCalculatedCorrectly(byte a, byte b, byte iterations, byte expectedValue)
    {
        var gameBoy = TestGameBoyBuilder
           .CreateBuilder()
           .WithProcessor(processor => processor
               .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
               .SetProgramCounter(0x0001))
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
        var instructionCount = 4 + 6 * iterations + 1;

        gameBoy.RunFor(instructionCount);
        
        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        Assert.Equal(expectedValue, processor.GetValueOfRegisterA());
    }
}