using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Schema;
using Xunit;
using Xunit.Abstractions;

namespace RetroEmu.Devices.Tests.PixelProcessingUnitTests;

public class WriteBackgroundTests(ITestOutputHelper output)
{
    [Theory]
    [InlineData(0x00, 0x00)]
    [InlineData(0x01, 0x02)]
    public void WriteBackground_WhenCalled_ShouldWriteBackground(byte SCX, byte SCY)
    {
        // Arrange
        IPixelProcessingUnit ppu = new PixelProcessingUnit();
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
        IPixelProcessingUnit ppu = new PixelProcessingUnit();
        ppu.WX = WX;
        ppu.WY = WY;
        ppu.EnableWindow();

        const byte tileIndex = 2;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + tileIndex * tileByteSize);
        byte[] sprite = [0x3C, 0x7E, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7E, 0x5E, 0x7E, 0x0A, 0x7C, 0x56, 0x38, 0x7C];

        foreach (var b in sprite)
        {
            ppu.WriteVRAM(tileStartAddress++, b);
        }

        const ushort winwoMapAddress = 0x9C00;
        const ushort xPos = 1;
        const ushort yPos = 2;
        const ushort bgMapWidth = 32;
        ppu.WriteVRAM(winwoMapAddress + yPos * bgMapWidth + xPos, tileIndex);

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
        IPixelProcessingUnit ppu = new PixelProcessingUnit();

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
}

public class PixelProcessingUnit : IPixelProcessingUnit
{
    private const ushort VramStartAddress = 0x8000;
    private const ushort bgMapAddressOffset = 0x9800 - VramStartAddress;
    private const ushort windowMapAddressOffset = 0x9C00 - VramStartAddress;
    private const ushort OamStartAddress = 0xFE00;
    private const ushort ScreenWidth = 160;
    private const ushort ScreenHeight = 160;

    private readonly byte[] _vram = new byte[0x2000];
    private readonly byte[] _oam = new byte[40 * 4];
    private readonly byte[] _pixelMemory = new byte[ScreenWidth * ScreenHeight];

    public byte LCDC { get; set; } = 0;
    public byte SCX { get; set; } = 0;
    public byte SCY { get; set; } = 0;

    public byte WX { get; set; } = 0;
    public byte WY { get; set; } = 0;

    private int _currentScanLineY = 0;
    private int _dotsSinceModeStart = 0;
    private PPUMode _currentMode = PPUMode.OAMScan;
    
    // Lage en Fetcher med FIFO-køer for OAM og BG/Window
    // https://gbdev.io/pandocs/pixel_fifo.html#vram-access

    public void EnableWindow()
    {
        LCDC |= 0x01 << 5;
    }

    private bool IsWindowEnabled()
    {
        return (LCDC & 0x01 << 5) > 0;
    }

    public void Update(int cycles)
    {
        var dots = cycles / 4;
        for (var dot = 0; dot < dots; dot++)
        {
            switch (_currentMode)
            {
                case PPUMode.OAMScan:
                    SearchForOverlappingObjects();
                    break;
                case PPUMode.VRAMRead:
                    UpdatePixelMemory();
                    break;
                case PPUMode.HBlank:
                case PPUMode.VBlank:
                default:
                    break;
            }
            UpdateMode();
        }
    }
    
    private struct Sprite
    {
        public byte YPos;
        public byte XPos;
        public byte TileIndex;
        public byte Flags;
    }
    
    private readonly List<Sprite> _sprites = [];
    
    private void SearchForOverlappingObjects()
    {
        if (_dotsSinceModeStart == 0)
        {
            _sprites.Clear();
        }
        
        if (_dotsSinceModeStart % 2 == 0)
        {
            return;
        }
        
        var objectOffset = _dotsSinceModeStart / 2 * 4;
        var yPos = _oam[objectOffset];
        var xPos = _oam[objectOffset + 1];
        var tileIndex = _oam[objectOffset + 2];
        var flags = _oam[objectOffset + 3];

        var overlapsCurrentScanLine = _currentScanLineY >= yPos - 16 && _currentScanLineY < yPos + 8 - 16;
        if (overlapsCurrentScanLine && _sprites.Count < 10)
        {
            _sprites.Add(new Sprite
            {
                YPos = yPos,
                XPos = xPos,
                TileIndex = tileIndex,
                Flags = flags
            });
        }
    }

    private void UpdatePixelMemory()
    {
        if (_dotsSinceModeStart != 0)
        {
            return;
        }

        for (var drawX = 0; drawX < 160; drawX++)
        {
            var drawY = _currentScanLineY;

            byte bgColor = 0;
            bool foundWindowColor = false;
            byte windowColor = 0;
            bool foundSpriteColor = false;
            byte spriteColor = 0;

            // Background fetch
            {
                var tileIndexX = (drawX + SCX) / 8;
                var tileIndexY = (drawY + SCY) / 8;
                var tileIndex = _vram[bgMapAddressOffset + tileIndexY * 32 + tileIndexX];
                var tileStartAddress = tileIndex * 16;

                var tileX = drawX + SCX - tileIndexX * 8;
                var tileY = drawY + SCY - tileIndexY * 8;

                var bytePos = tileY * 2;
                var bitPos = 7 - tileX;
                var colorBit1 = _vram[tileStartAddress + bytePos] >> bitPos & 0x01;
                var colorBit2 = _vram[tileStartAddress + bytePos + 1] >> bitPos & 0x01;
                var color = (colorBit2 << 1) | colorBit1;

                bgColor = (byte)color;
            }

            // Window fetch
            if (IsWindowEnabled() && drawX >= (WX - 7) && drawY >= WY)
            {
                var tileIndexX = (drawX - (WX - 7)) / 8;
                var tileIndexY = (drawY - WY) / 8;
                var tileIndex = _vram[windowMapAddressOffset + tileIndexY * 32 + tileIndexX];
                var tileStartAddress = tileIndex * 16;

                var tileX = drawX - (WX - 7) - tileIndexX * 8;
                var tileY = drawY - WY - tileIndexY * 8;

                var bytePos = tileY * 2;
                var bitPos = 7 - tileX;
                var colorBit1 = _vram[tileStartAddress + bytePos] >> bitPos & 0x01;
                var colorBit2 = _vram[tileStartAddress + bytePos + 1] >> bitPos & 0x01;
                var color = (colorBit2 << 1) | colorBit1;

                foundWindowColor = true;
                windowColor = (byte)color;
            }

            // Sprite fetch
            foreach (var sprite in _sprites)
            {
                var tileStartAddress = sprite.TileIndex * 16;
                var tileX = drawX - sprite.XPos;
                var tileY = drawY - (sprite.YPos - 16);

                if (tileX is < 0 or >= 8)
                {
                    continue;
                }
                
                var bytePos = tileY * 2;
                var bitPos = 7 - tileX;
                var colorBit1 = _vram[tileStartAddress + bytePos] >> bitPos & 0x01;
                var colorBit2 = _vram[tileStartAddress + bytePos + 1] >> bitPos & 0x01;
                var color = (colorBit2 << 1) | colorBit1;

                foundSpriteColor = true;
                spriteColor = (byte)color;
            }

            // TODO: Choose right one
            _pixelMemory[drawY * ScreenWidth + drawX] = (byte)bgColor;
            if (foundWindowColor)
            {
                _pixelMemory[drawY * ScreenWidth + drawX] = (byte)windowColor;
            }
            if (foundSpriteColor)
            {
                _pixelMemory[drawY * ScreenWidth + drawX] = (byte)spriteColor;
            }
        }
    }

    private void UpdateMode()
    {
        _dotsSinceModeStart++;
        
        if (_currentMode == PPUMode.OAMScan && _dotsSinceModeStart == 80)
        {
            _currentMode = PPUMode.VRAMRead;
            _dotsSinceModeStart = 0;
        }
        else if (_currentMode == PPUMode.VRAMRead && _dotsSinceModeStart == 172)
        {
            _currentMode = PPUMode.HBlank;
            _dotsSinceModeStart = 0;
        }
        else if (_currentMode == PPUMode.HBlank && _dotsSinceModeStart == 204)
        {
            _currentScanLineY++;
            if (_currentScanLineY == 144)
            {
                _currentMode = PPUMode.VBlank;
            }
            else
            {
                _currentMode = PPUMode.OAMScan;
            }
            _dotsSinceModeStart = 0;
        }
        else if (_currentMode == PPUMode.VBlank && _dotsSinceModeStart == 4560)
        {
            _currentScanLineY = 0;
            _currentMode = PPUMode.OAMScan;
            _dotsSinceModeStart = 0;
        }
    }

    public void WriteVRAM(ushort address, byte value)
    {
        // TODO: Check that address is within boundaries
        _vram[address - VramStartAddress] = value;
    }

    public void WriteOAM(ushort address, byte value)
    {
        // TODO: Check that address is within boundaries
        _oam[address - OamStartAddress] = value;
    }

    public byte ReadPixelMemory(int xPos, int yPos)
    {
        return _pixelMemory[yPos * ScreenWidth + xPos];
    }

    public void PrintPixelMemory()
    {
        for (int y = 0; y < ScreenHeight; y++)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                Console.Write(ReadPixelMemory(x, y) + " ");
            }
            Console.Write("\n");
        }
    }
}

internal enum PPUMode
{
    OAMScan = 2,
    VRAMRead = 3,
    HBlank = 0,
    VBlank = 1
}

public interface IPixelProcessingUnit
{
    public byte LCDC { get; set; }
    public byte SCX { get; set; }
    public byte SCY { get; set; }
    public byte WX { get; set; }
    public byte WY { get; set; }

    public void EnableWindow();
    public void Update(int cycles);
    public void WriteVRAM(ushort address, byte value);
    public void WriteOAM(ushort oamStartAddress, byte yPos);
    public byte ReadPixelMemory(int xPos, int yPos);
    public void PrintPixelMemory();
}