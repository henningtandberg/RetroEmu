using System;
using System.IO;
using RetroEmu.Devices.DMG.CPU.Interrupts;
using RetroEmu.Devices.DMG.CPU.PPU;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.GB.Tests.PixelProcessingUnitTests;

public class PixelProcessingUnitTests(ITestOutputHelper output)
{
    [Theory]
    [InlineData(0x00, 0x00)]
    [InlineData(0x01, 0x02)]
    public void WriteBackground_WhenCalled_ShouldWriteBackground(byte SCX, byte SCY)
    {
        // Arrange
        IPixelProcessingUnit ppu = new PixelProcessingUnit(new InterruptState());
        ppu.EnableBGAndWindow();
        ppu.SetTileAddressingMode1();
        ppu.SCX = SCX;
        ppu.SCY = SCY;

        const byte tileIndex = 2;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + tileIndex * tileByteSize);
        byte[] sprite = [0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C];

        foreach (var b in sprite)
        {
            ppu.WriteVRAM(tileStartAddress++, b);
        }

        const ushort bgMapAddress = 0x9800;
        const ushort xPos = 1;
        const ushort yPos = 2;
        const ushort bgMapWidth = 32;
        ppu.WriteVRAM(bgMapAddress + yPos * bgMapWidth + xPos, tileIndex);

        // Act
        const int cycles = 70224 * 4;
        ppu.Update(cycles);
        StringWriter sw = new();
        Console.SetOut(sw);
        ppu.PrintPixelMemory();
        output.WriteLine(sw.GetStringBuilder().ToString());

        // Assert
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
        for (var y = 0; y < tileSize; y++)
        {
            for (var x = 0; x < tileSize; x++)
            {
                var expectedColor = expectedPixelColorValues[y * 8 + x];
                var actualColor = ppu.ReadPixelMemory(xPos * tileSize + x - SCX, yPos * tileSize + y - SCY);
                Assert.Equal(expectedColor, actualColor);
            }
        }
    }

    [Theory]
    [InlineData(0x07, 0x00)]
    [InlineData(0x08, 0x02)]
    public void WriteWindow_WhenCalled_ShouldWriteWindow(byte WX, byte WY)
    {
        // Arrange
        IPixelProcessingUnit ppu = new PixelProcessingUnit(new InterruptState());
        ppu.EnableBGAndWindow();
        ppu.EnableWindow();
        ppu.SetTileAddressingMode1();
        ppu.WX = WX;
        ppu.WY = WY;

        const byte tileIndex = 2;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + tileIndex * tileByteSize);
        byte[] sprite = [0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C];

        foreach (var b in sprite)
        {
            ppu.WriteVRAM(tileStartAddress++, b);
        }

        const ushort windowMapAddress = 0x9800;
        const ushort xPos = 1;
        const ushort yPos = 2;
        const ushort bgMapWidth = 32;
        ppu.WriteVRAM(windowMapAddress + yPos * bgMapWidth + xPos, tileIndex);

        // Act
        const int cycles = 70224 * 4;
        ppu.Update(cycles);
        StringWriter sw = new();
        Console.SetOut(sw);
        ppu.PrintPixelMemory();
        output.WriteLine(sw.GetStringBuilder().ToString());

        // Assert
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
        for (var y = 0; y < tileSize; y++)
        {
            for (var x = 0; x < tileSize; x++)
            {
                var expectedColor = expectedPixelColorValues[y * 8 + x];
                var actualColor = ppu.ReadPixelMemory(xPos * tileSize + x + (WX - 7), yPos * tileSize + y + WY);
                Assert.Equal(expectedColor, actualColor);
            }
        }
    }

    [Fact]
    public void WriteSprite_WhenCalled_ShouldWriteSprite()
    {
        // Arrange
        IPixelProcessingUnit ppu = new PixelProcessingUnit(new InterruptState());
        ppu.EnableSprites();

        const byte tileIndex = 2;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + tileIndex * tileByteSize);
        byte[] sprite = [ 0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C ];
        
        foreach (var b in sprite)
        {
            ppu.WriteVRAM(tileStartAddress++, b);
        }

        const ushort oamStartAddress = 0xFE00;
        const byte xPos = 40;
        const byte yPos = 16;
        ppu.WriteOAM(oamStartAddress, yPos);
        ppu.WriteOAM(oamStartAddress + 1, xPos);
        ppu.WriteOAM(oamStartAddress + 2, tileIndex);
        
        // Act
        const int cycles = 70224 * 4;
        ppu.Update(cycles);
        StringWriter sw = new();
        Console.SetOut(sw);
        ppu.PrintPixelMemory();
        output.WriteLine(sw.GetStringBuilder().ToString());

        // Assert
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

    [Fact]
    public void WriteSprite8x16_WhenCalled_ShouldWriteSprite8x16()
    {
        // Arrange
        IPixelProcessingUnit ppu = new PixelProcessingUnit(new InterruptState());
        ppu.EnableSprites();
        ppu.EnableLargeSprites();

        const byte tileIndex = 3;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + 2 * tileByteSize);
        byte[] sprite = [
            0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C,
            0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C,
        ];

        foreach (var b in sprite)
        {
            ppu.WriteVRAM(tileStartAddress++, b);
        }

        const ushort oamStartAddress = 0xFE00;
        const byte xPos = 40;
        const byte yPos = 16;
        ppu.WriteOAM(oamStartAddress, yPos);
        ppu.WriteOAM(oamStartAddress + 1, xPos);
        ppu.WriteOAM(oamStartAddress + 2, tileIndex);

        // Act
        const int cycles = 70224 * 4;
        ppu.Update(cycles);
        StringWriter sw = new();
        Console.SetOut(sw);
        ppu.PrintPixelMemory();
        output.WriteLine(sw.GetStringBuilder().ToString());

        // Assert
        byte[] expectedPixelColorValues =
        [
            0, 2, 3, 3, 3, 3, 2, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 1, 3, 3, 3, 3, 0,
            0, 1, 1, 1, 3, 1, 3, 0,
            0, 3, 1, 3, 1, 3, 2, 0,
            0, 2, 3, 3, 3, 2, 0, 0,
            0, 2, 3, 3, 3, 3, 2, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 0, 0, 0, 0, 3, 0,
            0, 3, 1, 3, 3, 3, 3, 0,
            0, 1, 1, 1, 3, 1, 3, 0,
            0, 3, 1, 3, 1, 3, 2, 0,
            0, 2, 3, 3, 3, 2, 0, 0
        ];

        for (var y = 0; y < 16; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                var expectedColor = expectedPixelColorValues[y * 8 + x];
                var actualColor = ppu.ReadPixelMemory(xPos + x, y);
                Assert.Equal(expectedColor, actualColor);
            }
        }
    }
}