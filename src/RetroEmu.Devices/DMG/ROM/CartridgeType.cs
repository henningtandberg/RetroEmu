namespace RetroEmu.Devices.DMG.ROM;

public enum CartridgeType : byte
{
    ROMOnly = 0x00,
    ROMMBC1 = 0x01,
    ROMMBC1RAM = 0x02,
    ROMMBC1RAMBattery = 0x03,
}