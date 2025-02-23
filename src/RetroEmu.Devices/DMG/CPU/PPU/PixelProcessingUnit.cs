using System;
using System.Collections.Generic;
using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG.CPU.PPU;

public class PixelProcessingUnit(IInterruptState interruptState) : IPixelProcessingUnit
{
    private const ushort VramStartAddress = 0x8000;
    private const ushort tileMapArea1Offset = 0x9800 - VramStartAddress;
    private const ushort tileMapArea2Offset = 0x9C00 - VramStartAddress;
    private const ushort OamStartAddress = 0xFE00;
    private const ushort ScreenWidth = 160;
    private const ushort ScreenHeight = 160;

    private readonly byte[] _vram = new byte[0x2000];
    private readonly byte[] _oam = new byte[40 * 4];
    private readonly byte[] _pixelMemory = new byte[ScreenWidth * ScreenHeight];

    private byte _stat = 0;

    public byte LCDC { get; set; } = 0;
    public byte SCX { get; set; } = 0;
    public byte SCY { get; set; } = 0;

    public byte WX { get; set; } = 0;
    public byte WY { get; set; } = 0;

    public byte LYC { get; set; } = 0;
    public byte STAT {
        get
        {
            // Clear bottom 3 bits
            byte stat = (byte)(_stat & 0b11111000);

            // Set bottom three bits
            if (_currentMode == PPUMode.HBlank)
            {
                stat = 0;
            }
            else if (_currentMode == PPUMode.VBlank)
            {
                stat = 1;
            }
            else if (_currentMode == PPUMode.OAMScan)
            {
                stat = 2;
            }
            else if (_currentMode == PPUMode.VRAMRead)
            {
                stat = 3;
            }

            if (_currentScanLineY == LYC)
            {
                stat |= 0x01 << 2;
            }

            return stat;
        }
        set
        {
            _stat = (byte)(value & 0b11111000);
        }
    }

    private int _currentScanLineY = 0;
    private int _dotsSinceModeStart = 0;
    private PPUMode _currentMode = PPUMode.OAMScan;
    private bool _vBlankTriggered = false;

    // Lage en Fetcher med FIFO-køer for OAM og BG/Window
    // https://gbdev.io/pandocs/pixel_fifo.html#vram-access

    public void EnableBGAndWindow()
    {
        LCDC |= 0x01 << 0;
    }
    public void EnableWindow()
    {
        LCDC |= 0x01 << 5;
    }
    public void EnableSprites()
    {
        LCDC |= 0x01 << 1;
    }
    public void EnableLargeSprites()
    {
        LCDC |= 0x01 << 2;
    }
    public void SetTileAddressingMode1()
    {
        LCDC |= 0x01 << 4;
    }

    private bool IsBGEnabled()
    {
        return (LCDC & (0x01 << 0)) > 0;
    }
    private bool IsWindowEnabled()
    {
        return ((LCDC & (0x01 << 5)) > 0) && ((LCDC & (0x01 << 0)) > 0);
    }
    private bool IsSpritesEnabled()
    {
        return (LCDC & (0x01 << 1)) > 0;
    }

    private ushort GetBGTileMapAddressOffset()
    {
        bool useArea2 = (LCDC & (0x01 << 3)) > 0;
        return useArea2 ? tileMapArea2Offset : tileMapArea1Offset;
    }

    private ushort GetWindowTileMapAddressOffset()
    {
        bool useArea2 = (LCDC & (0x01 << 6)) > 0;
        return useArea2 ? tileMapArea2Offset : tileMapArea1Offset;
    }

    private byte GetSpriteHeight()
    {
        bool use8x16 = (LCDC & (0x01 << 2)) > 0;
        return (byte)(use8x16 ? 16 : 8);
    }

    private byte LoadTileData(byte tileIndex, byte tileX, byte tileY, bool getSecondByte)
    {
        bool useAddressMode1 = (LCDC & (0x01 << 4)) > 0;
        ushort startAddress = (ushort)(useAddressMode1 ? 0x0000 : 0x1000);
        var tileAddressOffset = useAddressMode1 ? (tileIndex * 16) : ((sbyte)tileIndex * 16);
        var bytePos = tileY * 2;
        var bitPos = 7 - tileX;
        var byteOffset = getSecondByte ? 0x1 : 0x0;
        return (byte)(_vram[startAddress + tileAddressOffset + bytePos + byteOffset] >> bitPos & 0x01);
    }

    private bool GetSTATInterruptLine()
    {
        bool mode0Selected = (STAT & (0x01 << 3)) > 0;
        bool mode1Selected = (STAT & (0x01 << 4)) > 0;
        bool mode2Selected = (STAT & (0x01 << 5)) > 0;
        bool LYCIntSelected = (STAT & (0x01 << 6)) > 0;

        bool line = false;
        if (mode0Selected)
        {
            line |= _currentMode == PPUMode.HBlank;
        }
        if (mode1Selected)
        {
            line |= _currentMode == PPUMode.VBlank;
        }
        if (mode2Selected)
        {
            line |= _currentMode == PPUMode.OAMScan;
        }
        if (LYCIntSelected)
        {
            line |= _currentScanLineY == LYC;
        }
        return line;
    }

    public void Update(int cycles)
    {
        bool STATLineBefore = GetSTATInterruptLine();
        _vBlankTriggered = false;

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
        bool STATLineAfter = GetSTATInterruptLine();

        if (!STATLineBefore && STATLineAfter)
        {
            interruptState.GenerateInterrupt(InterruptType.LCDC);
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

        var overlapsCurrentScanLine = _currentScanLineY >= yPos - 16 && _currentScanLineY < yPos + GetSpriteHeight() - 16;
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
                var tileIndex = _vram[GetBGTileMapAddressOffset() + tileIndexY * 32 + tileIndexX];

                var tileX = (byte)(drawX + SCX - tileIndexX * 8);
                var tileY = (byte)(drawY + SCY - tileIndexY * 8);

                var colorBit1 = LoadTileData(tileIndex, tileX, tileY, false);
                var colorBit2 = LoadTileData(tileIndex, tileX, tileY, true);
                var color = (colorBit2 << 1) | colorBit1;

                bgColor = (byte)color;
            }

            // Window fetch
            if (IsWindowEnabled() && drawX >= (WX - 7) && drawY >= WY)
            {
                var tileIndexX = (drawX - (WX - 7)) / 8;
                var tileIndexY = (drawY - WY) / 8;
                var tileIndex = _vram[GetWindowTileMapAddressOffset() + tileIndexY * 32 + tileIndexX];

                var tileX = (byte)(drawX - (WX - 7) - tileIndexX * 8);
                var tileY = (byte)(drawY - WY - tileIndexY * 8);

                var colorBit1 = LoadTileData(tileIndex, tileX, tileY, false);
                var colorBit2 = LoadTileData(tileIndex, tileX, tileY, true);
                var color = (colorBit2 << 1) | colorBit1;

                foundWindowColor = true;
                windowColor = (byte)color;
            }

            // Sprite fetch
            if (IsSpritesEnabled())
            {
                foreach (var sprite in _sprites)
                {
                    var spriteIndex = GetSpriteHeight() == 8 ? sprite.TileIndex : sprite.TileIndex & 0xFE;
                    var tileStartAddress = spriteIndex * 16;
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
                _vBlankTriggered = true;
                interruptState.GenerateInterrupt(InterruptType.VBlank);
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
    
    public byte ReadVRAM(ushort address)
    {
        return _vram[address - VramStartAddress];
    }

    public void WriteOAM(ushort address, byte value)
    {
        // TODO: Check that address is within boundaries
        _oam[address - OamStartAddress] = value;
    }
    
    public byte ReadOAM(ushort address)
    {
        return _vram[address - OamStartAddress];
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

    public bool VBlankTriggered()
    {
        return _vBlankTriggered;
    }
}