namespace RetroEmu.Devices.DMG.CPU.PPU;

public interface IPixelProcessingUnit
{
    public byte LCDC { get; set; }
    public byte SCX { get; set; }
    public byte SCY { get; set; }
    public byte WX { get; set; }
    public byte WY { get; set; }

    public void EnableBGAndWindow();
    public void EnableWindow();
    public void EnableSprites();
    public void EnableLargeSprites();
    public void SetTileAddressingMode1();
    public void Update(int cycles);
    public void WriteVRAM(ushort address, byte value);
    public void WriteOAM(ushort oamStartAddress, byte yPos);
    public byte ReadPixelMemory(int xPos, int yPos);
    public void PrintPixelMemory();
}