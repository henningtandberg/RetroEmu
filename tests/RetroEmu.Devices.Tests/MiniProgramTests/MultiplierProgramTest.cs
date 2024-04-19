using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.Devices.Tests.Setup;
using Xunit;

namespace RetroEmu.Devices.Tests.MiniProgramTests;

public class MultiplierProgramTest
{
    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(0, 1, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(2, 2, 4)]
    [InlineData(10, 2, 20)]
    [InlineData(13, 15, 195)]
    [InlineData(2, 100, 200)]
    [InlineData(1, 100, 100)]
    [InlineData(100, 1, 100)]
    public static void
        MultiplierProgram_MultipliesTwoNumbersUsingALoop_ProductIsStoredCorrectlyInA(byte x, byte y, byte expectedProduct)
    {
        var gameBoy = TestGameBoyBuilder
           .CreateBuilder()
           .WithProcessor(processor => processor
               .Set8BitGeneralPurposeRegisters(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)
               .SetProgramCounter(0x0001))
           .WithMemory(() => new Dictionary<ushort, byte>
           {
               // A => General purpose register
               [0x0001] = Opcode.Ld_B_N8,   // B = x;
               [0x0002] = x,
               [0x0003] = Opcode.Ld_C_N8,   // C = y;
               [0x0004] = y,
               [0x0005] = Opcode.Ld_A_C,    // A = C;
               [0x0006] = Opcode.Cp_A_N8,   // if (y == 0) goto end
               [0x0007] = 0x00,
               [0x0008] = Opcode.JpZ_N16,
               [0x0009] = 0x17,
               [0x000A] = 0x00,
               [0x000B] = Opcode.Ld_A_B,    // A = B;
               [0x000C] = Opcode.Cp_A_N8,   // if (x == 0) goto end
               [0x000D] = 0x00,
               [0x000E] = Opcode.JpZ_N16,
               [0x000F] = 0x17,
               [0x0010] = 0x00,
               [0x0011] = Opcode.Ld_A_N8,   // A = 0;
               [0x0012] = 0x00,
               [0x0013] = Opcode.Add_A_B,   // A = A + B;
               [0x0014] = Opcode.Dec_C,     // Decrement C until zero
               [0x0015] = Opcode.JpNZ_N16,  // Jump to 0x0013
               [0x0016] = 0x13,
               [0x0017] = 0x00,
               [0x0018] = Opcode.Nop        // END
           })
           .BuildGameBoy();

        var processor = gameBoy.GetProcessor();
        gameBoy.RunWhile(() => processor.GetValueOfRegisterPC() < 0x17);
        
        var actualProduct = processor.GetValueOfRegisterA();
        Assert.Equal(expectedProduct, actualProduct);
    }
}