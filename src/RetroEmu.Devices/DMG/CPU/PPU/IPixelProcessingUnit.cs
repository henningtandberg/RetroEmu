namespace RetroEmu.Devices.DMG.CPU.PPU;

public interface IPixelProcessingUnit
{
    public byte LCDC { get; set; }
    public byte SCX { get; set; }
    public byte SCY { get; set; }
    public byte WX { get; set; }
    public byte WY { get; set; }
    public byte LY { get; }
    public byte STAT { get; set; }
    public byte LYC { get; set; }

    public void EnableBGAndWindow();
    public void EnableWindow();
    public void EnableSprites();
    public void EnableLargeSprites();
    public void SetTileAddressingMode1();
    public void Update(int cycles);
    public void WriteVRAM(ushort address, byte value);
    public byte ReadVRAM(ushort address);
    public void WriteOAM(ushort oamStartAddress, byte yPos);
    public byte ReadOAM(ushort address);
    public byte ReadPixelMemory(int xPos, int yPos);
    public void PrintPixelMemory();
    public bool VBlankTriggered();
    void StartDMATransfer(byte value, IAddressBus addressBus);
}