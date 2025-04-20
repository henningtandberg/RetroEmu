using System;
using System.Collections.Generic;
using System.IO;
using RetroEmu.Devices.DMG;
using RetroEmu.Devices.DMG.CPU;
using RetroEmu.GB.TestSetup;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.GB.Tests.PixelProcessingUnitTests;

public class PixelProcessingUnitWireUpTest(ITestOutputHelper output)
{
    private const byte XPos = 40;
    private const byte YPos = 16;
    private const byte TileIndex = 2;
    private const byte TileByteSize = 16;
    
    private const ushort TileStartAddress = 0x8000 + TileIndex * TileByteSize;
    private const ushort OamStartAddress = 0xFE00;
    
    private static readonly byte[] Sprite =
    [ 
        0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C
    ];
    
    private readonly byte[] _writeSpriteCartridge = CartridgeBuilder
        .Create()
        .WithProgram([
            // Disable LDC
            Opcode.Ld_A_N8,
            0x00,
            Opcode.Ld_XN8_A,
            0x40,
            // Load Tile Data into VRAM
            Opcode.Ld_HL_N16,
            LowerByte(TileStartAddress),
            UpperByte(TileStartAddress),
            Opcode.Ld_XHL_N8,
            Sprite[0],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[1],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[2],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[3],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[4],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[5],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[6],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[7],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[8],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[9],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[10],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[11],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[12],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[13],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[14],
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            Sprite[15],
            Opcode.Inc_HL,
            // Load OAM Data into OAM
            Opcode.Ld_HL_N16,
            LowerByte(OamStartAddress),
            UpperByte(OamStartAddress),
            Opcode.Ld_XHL_N8,
            YPos,
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            XPos,
            Opcode.Inc_HL,
            Opcode.Ld_XHL_N8,
            TileIndex,
            // Disable LDC
            Opcode.Ld_A_N8,
            0b10000010,
            Opcode.Ld_XN8_A,
            0x40,
            Opcode.DI,
            Opcode.Halt
        ])
        .Build();
    
    private readonly IGameBoy _gameBoy = TestGameBoyBuilder
        .CreateBuilder()
        .BuildGameBoy();
    
    private static byte LowerByte(ushort value) => (byte)(value & 0xFF);
    private static byte UpperByte(ushort value) => (byte)((value >> 8) & 0xFF);
    
    [Fact]
    public void WriteSprite_WhenCalled_ShouldWriteSprite()
    {
        _gameBoy.Load(_writeSpriteCartridge);
        var processor = (ITestableProcessor)_gameBoy.GetProcessor();
        
        // Run until HALT
        while (processor.GetValueOfRegisterPC() != 0x0197)
        {
            _ = _gameBoy.Update();
        }
        
        // Let LCD finish rendering
        var cycles = 70224 * 4;
        while (cycles >= 0)
        {
            cycles -= _gameBoy.Update();
        }

        // Assert
        var ppu = processor.GetPixelProcessingUnit();
        
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

        const int tileSize = 8;
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                var expectedColor  = expectedPixelColorValues[y * 8 + x];
                var actualColor = ppu.ReadPixelMemory(XPos - tileSize + x, y);
                Assert.Equal(expectedColor, actualColor);
            }
        }
    }
}