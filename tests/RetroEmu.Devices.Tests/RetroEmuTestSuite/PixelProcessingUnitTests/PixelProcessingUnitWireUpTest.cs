using System;
using System.Collections.Generic;
using System.IO;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.Devices.Tests.RetroEmuTestSuite.PixelProcessingUnitTests;

public class PixelProcessingUnitWireUpTest(ITestOutputHelper output)
{
    [Fact]
    public void WriteSprite_WhenCalled_ShouldWriteSprite()
    {
        const byte tileIndex = 2;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + tileIndex * tileByteSize);
        byte[] sprite = [ 0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C ];
        var oamStartAddress = 0xFE00;
        const byte xPos = 40;
        const byte yPos = 16;
        
        var gameBoy = TestGameBoyBuilder
            .CreateBuilder()
            .WithProcessor(processor => processor.SetProgramCounter(0x00FF))
            .WithMemory(() => new Dictionary<ushort, byte>
            {
                // Disable LDC
                [0x00FC] = Opcode.Ld_A_N8,
                [0x00FD] = 0x00,
                [0x00FE] = Opcode.Ld_XN8_A,
                [0x00FF] = 0x40,
                // Load Tile Data into VRAM
                [0x0100] = Opcode.Ld_HL_N16,
                [0x0101] = (byte)tileStartAddress,
                [0x0102] = (byte)(tileStartAddress >> 8),
                [0x0103] = Opcode.Ld_XHL_N8,
                [0x0104] = sprite[0],
                [0x0105] = Opcode.Inc_HL,
                [0x0106] = Opcode.Ld_XHL_N8,
                [0x0107] = sprite[1],
                [0x0108] = Opcode.Inc_HL,
                [0x0109] = Opcode.Ld_XHL_N8,
                [0x010A] = sprite[2],
                [0x010B] = Opcode.Inc_HL,
                [0x010C] = Opcode.Ld_XHL_N8,
                [0x010D] = sprite[3],
                [0x010E] = Opcode.Inc_HL,
                [0x010F] = Opcode.Ld_XHL_N8,
                [0x0110] = sprite[4],
                [0x0111] = Opcode.Inc_HL,
                [0x0112] = Opcode.Ld_XHL_N8,
                [0x0113] = sprite[5],
                [0x0114] = Opcode.Inc_HL,
                [0x0115] = Opcode.Ld_XHL_N8,
                [0x0116] = sprite[6],
                [0x0117] = Opcode.Inc_HL,
                [0x0118] = Opcode.Ld_XHL_N8,
                [0x0119] = sprite[7],
                [0x011A] = Opcode.Inc_HL,
                [0x011B] = Opcode.Ld_XHL_N8,
                [0x011C] = sprite[8],
                [0x011D] = Opcode.Inc_HL,
                [0x011E] = Opcode.Ld_XHL_N8,
                [0x011F] = sprite[9],
                [0x0120] = Opcode.Inc_HL,
                [0x0121] = Opcode.Ld_XHL_N8,
                [0x0122] = sprite[10],
                [0x0123] = Opcode.Inc_HL,
                [0x0124] = Opcode.Ld_XHL_N8,
                [0x0125] = sprite[11],
                [0x0126] = Opcode.Inc_HL,
                [0x0127] = Opcode.Ld_XHL_N8,
                [0x0128] = sprite[12],
                [0x0129] = Opcode.Inc_HL,
                [0x012A] = Opcode.Ld_XHL_N8,
                [0x012B] = sprite[13],
                [0x012C] = Opcode.Inc_HL,
                [0x012D] = Opcode.Ld_XHL_N8,
                [0x012E] = sprite[14],
                [0x012F] = Opcode.Inc_HL,
                [0x0130] = Opcode.Ld_XHL_N8,
                [0x0131] = sprite[15],
                [0x0132] = Opcode.Inc_HL,
                // Load OAM Data into OAM
                [0x0133] = Opcode.Ld_HL_N16,
                [0x0134] = (byte)oamStartAddress,
                [0x0135] = (byte)(oamStartAddress >> 8),
                [0x0136] = Opcode.Ld_XHL_N8,
                [0x0137] = yPos,
                [0x0138] = Opcode.Inc_HL,
                [0x0139] = Opcode.Ld_XHL_N8,
                [0x013A] = xPos,
                [0x013B] = Opcode.Inc_HL,
                [0x013C] = Opcode.Ld_XHL_N8,
                [0x013D] = tileIndex,
                // Disable LDC
                [0x013E] = Opcode.Ld_A_N8,
                [0x013F] = 0b10000010,
                [0x0140] = Opcode.Ld_XN8_A,
                [0x0141] = 0x40,
                [0x0142] = Opcode.DI,
                [0x0143] = Opcode.Halt
            })
            .BuildGameBoy();

        var processor = (ITestableProcessor)gameBoy.GetProcessor();
        while (processor.GetValueOfRegisterPC() != 0x0143)
        {
            _ = gameBoy.Update();
        }
        
        var cycles = 70224 * 4;
        while (cycles >= 0)
        {
            cycles -= gameBoy.Update();
        }

        // Assert
        var ppu = ((ITestableProcessor)gameBoy.GetProcessor()).GetPixelProcessingUnit();
        
        StringWriter sw = new();
        Console.SetOut(sw);
        ppu.PrintPixelMemory();
        output.WriteLine(sw.GetStringBuilder().ToString());
        
        byte[] expectedPixelColorValues =
        [
            0, 2, 3, 3, 3, 3, 2, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 1, 3, 3, 3, 3, 0,
            0, 1, 1, 1, 3, 1, 3, 0,
            0, 3, 1, 3, 1, 3, 2, 0,
            0, 2, 3, 3, 3, 2, 0, 0
        ];

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                var expectedColor  = expectedPixelColorValues[y * 8 + x];
                var actualColor = ppu.ReadPixelMemory(xPos + x, y);
                Assert.Equal(expectedColor, actualColor);
            }
        }
    }
}