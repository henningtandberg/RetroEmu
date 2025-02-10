namespace RetroEmu.Devices.DMG.CPU.Interrupts;

public enum InterruptType
{
    VBlank = 0x01,
    LCDC = 0x02,
    Timer = 0x04,
    Serial = 0x08,
    Button = 0x10,
}