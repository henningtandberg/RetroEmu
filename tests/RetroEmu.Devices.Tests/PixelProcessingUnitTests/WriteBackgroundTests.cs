using Xunit;

namespace RetroEmu.Devices.Tests.PixelProcessingUnitTests;

public class WriteBackgroundTests
{
    [Fact]
    public void WriteBackground_WhenCalled_ShouldWriteBackground()
    {
        // Arrange
        IPixelProcessingUnit ppu = new PixelProcessingUnit();
        const byte tileIndex = 2;
        const byte tileByteSize = 16;
        var tileStartAddress = (ushort)(0x8000 + tileIndex * tileByteSize);
        byte[] sprite =
        [
            0x3C, 0x7E, 0x42, 0x42,
            0x42, 0x42, 0x42, 0x42,
            0x7E, 0x5E, 0x7E, 0x0A,
            0x7C, 0x56, 0x38, 0x7C
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

        // Assert
        byte[] expected =
        [ 0x2F, 0xF8, 0x30, 0x0C, 0x30, 0x0C, 0x30, 0x0C, 0x37, 0xFC, 0x15, 0xDC, 0x37, 0x78, 0x2F, 0xE0 ];
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 2; x++)
            {
                for (var b = 0; b < 4; b++)
                {
                    var expectedColorByte = expected[y * 2 + x];
                    var expectedColor = expectedColorByte >> (6 - b * 2) & 0b11;
                    var actualColor = ppu.ReadPixelMemory(xPos + x * 2 + b, yPos + y);
                    Assert.Equal(expectedColor, actualColor);
                }
            }
        }
    }
}

public class PixelProcessingUnit : IPixelProcessingUnit
{
    private const ushort VramStartAddress = 0x8000;
    private const ushort OamStartAddress = 0xFE00;

    byte[] _vram = new byte[0x2000];
    byte[] _oam = new byte[0xA0];

    private int currentScanLine = 0;
    private int dotsSinceModeStart = 0;
    private PPUMode currentMode = PPUMode.OAMScan;

    public void Update(int cycles)
    {
        var dots = cycles / 4;
        for (var dot = 0; dot < dots; dot++)
        {
            dotsSinceModeStart++;
            UpdateMode();
        }
    }

    private void UpdateMode()
    {
        if (currentMode == PPUMode.OAMScan && dotsSinceModeStart == 80)
        {
            currentMode = PPUMode.VRAMRead;
            dotsSinceModeStart = 0;
        }
        else if (currentMode == PPUMode.VRAMRead && dotsSinceModeStart == 172)
        {
            currentMode = PPUMode.HBlank;
            dotsSinceModeStart = 0;
        }
        else if (currentMode == PPUMode.HBlank && dotsSinceModeStart == 204)
        {
            currentScanLine++;
            if (currentScanLine == 144)
            {
                currentMode = PPUMode.VBlank;
            }
            else
            {
                currentMode = PPUMode.OAMScan;
            }
            dotsSinceModeStart = 0;
        }
        else if (currentMode == PPUMode.VBlank && dotsSinceModeStart == 4560)
        {
            currentScanLine = 0;
            currentMode = PPUMode.OAMScan;
            dotsSinceModeStart = 0;
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
        throw new System.NotImplementedException();
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
    public void Update(int cycles);
    public void WriteVRAM(ushort address, byte value);
    public void WriteOAM(ushort oamStartAddress, byte yPos);
    public byte ReadPixelMemory(int xPos, int yPos);
}